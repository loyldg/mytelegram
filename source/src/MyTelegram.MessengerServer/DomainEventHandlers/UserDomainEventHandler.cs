namespace MyTelegram.MessengerServer.DomainEventHandlers;

public class UserDomainEventHandler : DomainEventHandlerBase,
    ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>,
    ISubscribeSynchronousTo<UserAggregate, UserId, UserProfileUpdatedEvent>,
    ISubscribeSynchronousTo<UserAggregate, UserId, UserNameUpdatedEvent>,
    ISubscribeSynchronousTo<UserAggregate, UserId, UserProfilePhotoChangedEvent>
{
    private readonly ITlAuthorizationConverter _authorizationConverter;
    private readonly IEventBus _eventBus;
    private readonly ILogger<UserDomainEventHandler> _logger;
    private readonly IQueryProcessor _queryProcessor;
    private readonly ITlUserConverter _userConverter;

    public UserDomainEventHandler(IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService,
        IEventBus eventBus,
        ITlUserConverter userConverter,
        IQueryProcessor queryProcessor,
        ILogger<UserDomainEventHandler> logger,
        ITlAuthorizationConverter authorizationConverter) : base(objectMessageSender,
        commandBus,
        idGenerator,
        ackCacheService,
        responseCacheAppService)
    {
        _eventBus = eventBus;
        _userConverter = userConverter;
        _queryProcessor = queryProcessor;
        _logger = logger;
        _authorizationConverter = authorizationConverter;
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "User created,userId={UserId},phoneNumber={PhoneNumber},firstName={FirstName},lastName={LastName}",
            domainEvent.AggregateEvent.UserId,
            domainEvent.AggregateEvent.PhoneNumber,
            domainEvent.AggregateEvent.FirstName,
            domainEvent.AggregateEvent.LastName
        );

        await _eventBus.PublishAsync(new UserSignUpSuccessIntegrationEvent(domainEvent.AggregateEvent.Request.AuthKeyId,
            domainEvent.AggregateEvent.Request.PermAuthKeyId,
            domainEvent.AggregateEvent.UserId)).ConfigureAwait(false);

        var r = _authorizationConverter.CreateAuthorizationFromUser(domainEvent.AggregateEvent);
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.Request.ReqMsgId,
            r,
            domainEvent.Metadata.SourceId.Value,
            domainEvent.AggregateEvent.UserId).ConfigureAwait(false);
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserNameUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var r = _userConverter.ToUser(domainEvent.AggregateEvent);

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r).ConfigureAwait(false);
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserProfilePhotoChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var r = _userConverter.ToUserPhoto(domainEvent.AggregateEvent);

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r).ConfigureAwait(false);
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserProfileUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        // todo:这里直接通过事件信息创建用户对象，不从ReadModel读取
        var user = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(domainEvent.AggregateEvent.UserId),
            cancellationToken).ConfigureAwait(false);
        var r = _userConverter.ToUser(user!, user!.UserId);

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
            r,
            domainEvent.Metadata.SourceId.Value,
            domainEvent.AggregateEvent.UserId).ConfigureAwait(false);
    }
}
