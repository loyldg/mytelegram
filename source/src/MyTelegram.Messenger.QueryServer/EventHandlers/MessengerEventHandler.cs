namespace MyTelegram.Messenger.QueryServer.EventHandlers;

public class MessengerEventHandler : 
    //IEventHandler<MessengerDataReceivedEvent>,
    IEventHandler<MessengerQueryDataReceivedEvent>,
    IEventHandler<StickerDataReceivedEvent>
    //,

//IEventHandler<BotCommandResponse>,
////IEventHandler<NewDeviceCreatedEvent>,
//IEventHandler<BindUidToAuthKeyIntegrationEvent>,
//IEventHandler<AuthKeyUnRegisteredIntegrationEvent>,
//IEventHandler<DuplicateCommandEvent>,
//IEventHandler<CreateBotUserRequest>,
//IEventHandler<DeleteBotUserRequest>,
//IEventHandler<BotPicChangedEvent>
//IDistributedEventHandler<UserIsOnlineEvent>,
{
    private readonly ICommandBus _commandBus;
    private readonly ILogger<MessengerEventHandler> _logger;
    private readonly IObjectMessageSender _objectMessageSender;
    private readonly IMessageQueueProcessor<MessengerQueryDataReceivedEvent> _processor;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IRandomHelper _randomHelper;

    private static long _count = 0;

    public MessengerEventHandler(ICommandBus commandBus,
        IMessageQueueProcessor<MessengerQueryDataReceivedEvent> processor,
        IObjectMessageSender objectMessageSender,
        ILogger<MessengerEventHandler> logger,
        IQueryProcessor queryProcessor,
        IRandomHelper randomHelper)
    {
        _commandBus = commandBus;
        _processor = processor;
        _objectMessageSender = objectMessageSender;
        _logger = logger;
        _queryProcessor = queryProcessor;
        _randomHelper = randomHelper;
    }

    public Task HandleEventAsync(UserIsOnlineEvent eventData)
    {
        _logger.LogDebug("User {UserId} is online,tempAuthKeyId={TempAuthKeyId:x2},permAuthKeyId={PermAuthKeyId:x2}",
            eventData.UserId,
            eventData.TempAuthKeyId,
            eventData.PermAuthKeyId);
        var updatesTooLong = new TUpdatesTooLong();
        return _objectMessageSender.PushSessionMessageToAuthKeyIdAsync(eventData.TempAuthKeyId, updatesTooLong);
    }

    public Task HandleEventAsync(MessengerQueryDataReceivedEvent eventData)
    {
        _processor.Enqueue(eventData,eventData.PermAuthKeyId);
        return Task.CompletedTask;
    }

    public Task HandleEventAsync(StickerDataReceivedEvent eventData)
    {
        _processor.Enqueue(
            new MessengerQueryDataReceivedEvent(eventData.ConnectionId, eventData.RequestId, eventData.ObjectId,
                eventData.UserId, eventData.ReqMsgId, eventData.SeqNumber, eventData.AuthKeyId, eventData.PermAuthKeyId,
                eventData.Data, eventData.Layer, eventData.Date), eventData.AuthKeyId);
        return Task.CompletedTask;
    }
}
