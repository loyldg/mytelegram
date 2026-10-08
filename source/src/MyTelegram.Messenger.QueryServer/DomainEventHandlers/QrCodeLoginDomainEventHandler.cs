namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class QrCodeLoginDomainEventHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IQueryProcessor queryProcessor,
    ILayeredService<IAuthorizationConverter> authorizationLayeredService,
    ILogger<QrCodeLoginDomainEventHandler> logger)
    : DomainEventHandlerBase(objectMessageSender,
            commandBus,
            idGenerator,
            ackCacheService),
        ISubscribeAsynchronousTo<QrCodeAggregate, QrCodeId, QrCodeLoginTokenExportedEvent>,
        ISubscribeAsynchronousTo<QrCodeAggregate, QrCodeId, LoginTokenAcceptedEvent>
{
    private readonly IObjectMessageSender _objectMessageSender = objectMessageSender;

    public async Task HandleAsync(IDomainEvent<QrCodeAggregate, QrCodeId, LoginTokenAcceptedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var deviceReadModel = await queryProcessor
            .ProcessAsync(new GetDeviceByAuthKeyIdQuery(domainEvent.AggregateEvent.QrCodeLoginRequestPermAuthKeyId), cancellationToken);

        if (deviceReadModel == null)
        {
            logger.LogWarning(
                "Get device info failed, permAuthKeyId: {PermAuthKeyId:x2}",
                domainEvent.AggregateEvent.QrCodeLoginRequestPermAuthKeyId);
            return;
        }
        

        var authorization = authorizationLayeredService
            .GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToAuthorization(deviceReadModel);
        //var authorization = authorizationLayeredService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
        //    .Convert(deviceReadModel);

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, authorization);

        var updateShortForLoginWithTokenRequestOwner =
            new TUpdateShort { Date = DateTime.UtcNow.ToTimestamp(), Update = new TUpdateLoginToken() };

        await _objectMessageSender
            .PushSessionMessageToAuthKeyIdAsync(domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId,
                updateShortForLoginWithTokenRequestOwner);
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

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }
}
