using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.Services.Interfaces;
using MyTelegram.Messenger.TLObjectConverters.Interfaces;
using MyTelegram.Services.TLObjectConverters;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class UserDomainEventHandler : DomainEventHandlerBase,
    ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>,
    ISubscribeSynchronousTo<UserAggregate, UserId, UserProfileUpdatedEvent>,
    ISubscribeSynchronousTo<UserAggregate, UserId, UserNameUpdatedEvent>,
    ISubscribeSynchronousTo<UserAggregate, UserId, UserProfilePhotoChangedEvent>,
    ISubscribeSynchronousTo<UserAggregate, UserId, UserProfilePhotoUploadedEvent>
{
    //private readonly ITlAuthorizationConverter _authorizationConverter;
    private readonly IEventBus _eventBus;
    private readonly ILogger<UserDomainEventHandler> _logger;
    private readonly IQueryProcessor _queryProcessor;
    //private readonly ITlUserConverter _userConverter;
    private readonly ILayeredService<IUserConverter> _layeredUserService;
    private readonly ILayeredService<IAuthorizationConverter> _layeredAuthorizationService;
    private readonly ILayeredService<IPhotoConverter> _layeredPhotoService;
    private readonly IPhotoAppService _photoAppService;


    public UserDomainEventHandler(IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService,
        IEventBus eventBus,
        IQueryProcessor queryProcessor,
        ILogger<UserDomainEventHandler> logger,
        ILayeredService<IUserConverter> layeredUserService,
        ILayeredService<IAuthorizationConverter> layeredAuthorizationService,
        ILayeredService<IPhotoConverter> layeredPhotoService,
        IPhotoAppService photoAppService
        ) : base(objectMessageSender,
        commandBus,
        idGenerator,
        ackCacheService,
        responseCacheAppService)
    {
        _eventBus = eventBus;
        _queryProcessor = queryProcessor;
        _logger = logger;
        _layeredUserService = layeredUserService;
        _layeredAuthorizationService = layeredAuthorizationService;
        _photoAppService = photoAppService;
        _layeredPhotoService = layeredPhotoService;
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

        await _eventBus.PublishAsync(new UserSignUpSuccessIntegrationEvent(domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
            domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId,
            domainEvent.AggregateEvent.UserId));
        var user = _layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToUser(domainEvent.AggregateEvent);
        var r = _layeredAuthorizationService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).CreateAuthorization(user);
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
            r,
            domainEvent.AggregateEvent.UserId);
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserNameUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var user = await _queryProcessor.ProcessAsync(
            new GetUserByIdQuery(domainEvent.AggregateEvent.RequestInfo.UserId), cancellationToken);
        var photos = await _photoAppService.GetPhotosAsync(user);
        var r = _layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToUser(domainEvent.AggregateEvent.RequestInfo.UserId, user!, photos);

        //var r = _layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).ToUser(domainEvent.AggregateEvent);
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserProfilePhotoChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var photo = await _photoAppService.GetPhotoAsync(domainEvent.AggregateEvent.PhotoId);
        var userReadModel = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(domainEvent.AggregateEvent.UserId), cancellationToken);
        var r = _layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).ToUserPhoto(userReadModel!, photo);

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserProfilePhotoUploadedEvent> domainEvent, CancellationToken cancellationToken)
    {
        var photoReadModel = await _photoAppService.GetPhotoAsync(domainEvent.AggregateEvent.PhotoId);
        var userReadModel =
            await _queryProcessor.ProcessAsync(new GetUserByIdQuery(domainEvent.AggregateEvent.RequestInfo.UserId), cancellationToken);

        var r = _layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).ToUserPhoto(userReadModel!, photoReadModel);

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserProfileUpdatedEvent> domainEvent,
            CancellationToken cancellationToken)
    {
        var userReadModel = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(domainEvent.AggregateEvent.UserId),
            cancellationToken);

        var photos = await _photoAppService.GetPhotosAsync(userReadModel);

        var r = _layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).ToUser(userReadModel!.UserId, userReadModel, photos);

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
            r,
            domainEvent.AggregateEvent.UserId);
    }
}

