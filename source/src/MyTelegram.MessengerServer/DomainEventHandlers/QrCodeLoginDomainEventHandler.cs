using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.DomainEventHandlers;

public class QrCodeLoginDomainEventHandler : DomainEventHandlerBase,
    ISubscribeSynchronousTo<QrCodeAggregate, QrCodeId, QrCodeLoginTokenExportedEvent>,
    ISubscribeSynchronousTo<QrCodeAggregate, QrCodeId, LoginTokenAcceptedEvent>
{
    private readonly ITlAuthorizationConverter _authorizationConverter;
    private readonly ILogger<QrCodeLoginDomainEventHandler> _logger;
    private readonly ILoginTokenCacheAppService _loginTokenCacheAppService;
    private readonly IObjectMessageSender _objectMessageSender;
    private readonly IQueryProcessor _queryProcessor;

    public QrCodeLoginDomainEventHandler(IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService,
        IQueryProcessor queryProcessor,
        ILogger<QrCodeLoginDomainEventHandler> logger,
        ILoginTokenCacheAppService loginTokenCacheAppService,
        ITlAuthorizationConverter authorizationConverter) : base(objectMessageSender,
        commandBus,
        idGenerator,
        ackCacheService,
        responseCacheAppService)
    {
        _objectMessageSender = objectMessageSender;
        _queryProcessor = queryProcessor;
        _logger = logger;
        _loginTokenCacheAppService = loginTokenCacheAppService;
        _authorizationConverter = authorizationConverter;
    }

    public async Task HandleAsync(IDomainEvent<QrCodeAggregate, QrCodeId, LoginTokenAcceptedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        // 这里最好不要从ReadModel读取设备信息
        var deviceReadModel = await _queryProcessor
            .ProcessAsync(new GetDeviceByAuthKeyIdQuery(domainEvent.AggregateEvent.QrCodeLoginRequestPermAuthKeyId),
                default);

        if (deviceReadModel == null)
        {
            _logger.LogWarning(
                "Get device info failed.perm authKeyId={PermAuthKeyId:x2}",
                domainEvent.AggregateEvent.QrCodeLoginRequestPermAuthKeyId);
            return;
        }

        _loginTokenCacheAppService.AddLoginSuccessAuthKeyIdToCache(
            domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId,
            domainEvent.AggregateEvent.UserId);
        var authorization = _authorizationConverter.ToAuthorization(deviceReadModel);
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, authorization)
            ;

        var updateShortForLoginWithTokenRequestOwner =
            new TUpdateShort { Date = DateTime.UtcNow.ToTimestamp(), Update = new TUpdateLoginToken() };
        // send updates to qr code client

        await _objectMessageSender
            .PushSessionMessageToAuthKeyIdAsync(domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId,
                updateShortForLoginWithTokenRequestOwner);
        //await PushUpdatesToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.UserId),
        //    updateShortForLoginWithTokenRequestOwner,
        //    excludeUid:-1,
        //    onlySendToThisAuthKeyId: domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId);
        _logger.LogDebug("Accept qr code login token,userId={UserId},qr code client authKeyId={AuthKeyId}",
            domainEvent.AggregateEvent.UserId,
            domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId);
        //await SendMessageToAuthKeyIdAsync(domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId,
        //    updateShortForLoginWithTokenRequestOwner);
    }

    public async Task HandleAsync(
        IDomainEvent<QrCodeAggregate, QrCodeId, QrCodeLoginTokenExportedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var r = new TLoginToken
        {
            Token = domainEvent.AggregateEvent.Token,
            Expires = domainEvent.AggregateEvent.ExpireDate
        };

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r);
    }
}
