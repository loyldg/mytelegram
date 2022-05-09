namespace MyTelegram.MessengerServer.Abp.EventHandlers;

public class MessengerEventHandler : IDistributedEventHandler<MessengerDataReceivedEvent>,
    IDistributedEventHandler<NewDeviceCreatedEvent>,
    IDistributedEventHandler<BindUidToAuthKeyIntegrationEvent>,
    IDistributedEventHandler<AuthKeyUnRegisteredIntegrationEvent>,
    IDistributedEventHandler<DuplicateCommandEvent>,
//IDistributedEventHandler<UserIsOnlineEvent>,
    ITransientDependency
{
    private readonly ICommandBus _commandBus;
    private readonly ILogger<MessengerEventHandler> _logger;
    private readonly IObjectMessageSender _objectMessageSender;
    private readonly IMessageQueueProcessor<MessengerDataReceivedEvent> _processor;
    private readonly IQueryProcessor _queryProcessor;

    public MessengerEventHandler(ICommandBus commandBus,
        IMessageQueueProcessor<MessengerDataReceivedEvent> processor,
        IObjectMessageSender objectMessageSender,
        ILogger<MessengerEventHandler> logger,
        IQueryProcessor queryProcessor)
    {
        _commandBus = commandBus;
        _processor = processor;
        _objectMessageSender = objectMessageSender;
        _logger = logger;
        _queryProcessor = queryProcessor;
    }

    public Task HandleEventAsync(AuthKeyUnRegisteredIntegrationEvent eventData)
    {
        var command = new UnRegisterDeviceForAuthKeyCommand(DeviceId.Create(eventData.PermAuthKeyId),
            eventData.PermAuthKeyId,
            eventData.TempAuthKeyId);
        return _commandBus.PublishAsync(command, default);
    }

    public Task HandleEventAsync(BindUidToAuthKeyIntegrationEvent eventData)
    {
        var command = new BindUidToDeviceCommand(DeviceId.Create(eventData.PermAuthKeyId),
            eventData.UserId,
            eventData.PermAuthKeyId);
        return _commandBus.PublishAsync(command, default);
    }

    public async Task HandleEventAsync(DuplicateCommandEvent eventData)
    {
        var rpcResultReadModel =
            await _queryProcessor
                .ProcessAsync(new GetRpcResultByIdQuery(RpcResultId.Create(eventData.SourceId).Value), default)
                .ConfigureAwait(false);
        if (rpcResultReadModel != null)
        {
            var rpcData = rpcResultReadModel.RpcData.ToTObject<IObject>();
            await _objectMessageSender.SendRpcMessageToClientAsync(eventData.ReqMsgId, rpcData).ConfigureAwait(false);
        }
        else
        {
            _logger.LogWarning("Can not find rpc result for source id:{CommandId}", eventData.SourceId);
        }
    }

    public Task HandleEventAsync(MessengerDataReceivedEvent eventData)
    {
        _processor.Enqueue(eventData, eventData.AuthKeyId);
        return Task.CompletedTask;
    }

    public async Task HandleEventAsync(NewDeviceCreatedEvent eventData)
    {
        try
        {
            var createDeviceCommand = new CreateDeviceCommand(DeviceId.Create(eventData.PermAuthKeyId),
                eventData.ReqMsgId,
                eventData.PermAuthKeyId,
                eventData.TempAuthKeyId,
                eventData.UserId,
                eventData.AppId,
                eventData.AppVersion,
                eventData.AppVersion,
                eventData.Hash,
                eventData.OfficialApp,
                eventData.PasswordPending,
                eventData.DeviceModel,
                eventData.Platform,
                eventData.SystemVersion,
                eventData.SystemLangCode,
                eventData.LangPack,
                eventData.LangCode,
                eventData.Ip,
                eventData.Layer
            );
            await _commandBus.PublishAsync(createDeviceCommand, default).ConfigureAwait(false);
        }
        catch (DuplicateOperationException)
        {
            // Ignore duplicate exception
        }
    }

    //public Task HandleEventAsync(UserIsOnlineEvent eventData)
    //{
    //    _logger.LogDebug("User {UserId} is online,tempAuthKeyId={TempAuthKeyId:x2},permAuthKeyId={PermAuthKeyId:x2}",
    //        eventData.UserId,
    //        eventData.TempAuthKeyId,
    //        eventData.PermAuthKeyId);
    //    var updatesTooLong = new TUpdatesTooLong();
    //    return _objectMessageSender.PushSessionMessageToAuthKeyIdAsync(eventData.TempAuthKeyId, updatesTooLong);
    //}
}
