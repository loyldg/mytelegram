namespace MyTelegram.Messenger.QueryServer.EventHandlers;

public class MessengerEventHandler :
    IEventHandler<MessengerQueryDataReceivedEvent>,
    IEventHandler<StickerDataReceivedEvent>
{
    private readonly ILogger<MessengerEventHandler> _logger;
    private readonly IObjectMessageSender _objectMessageSender;
    private readonly IMessageQueueProcessor<MessengerQueryDataReceivedEvent> _processor;


    public MessengerEventHandler(
        IMessageQueueProcessor<MessengerQueryDataReceivedEvent> processor,
        IObjectMessageSender objectMessageSender,
        ILogger<MessengerEventHandler> logger)
    {
        _processor = processor;
        _objectMessageSender = objectMessageSender;
        _logger = logger;
    }

    public Task HandleEventAsync(MessengerQueryDataReceivedEvent eventData)
    {
        _processor.Enqueue(eventData, eventData.PermAuthKeyId);
        return Task.CompletedTask;
    }

    public Task HandleEventAsync(StickerDataReceivedEvent eventData)
    {
        _processor.Enqueue(
            new MessengerQueryDataReceivedEvent(eventData.ConnectionId, eventData.RequestId, eventData.ObjectId,
                eventData.UserId, eventData.ReqMsgId, eventData.SeqNumber, eventData.AuthKeyId, eventData.PermAuthKeyId,
                eventData.Data, eventData.Layer, eventData.Date, eventData.DeviceType, eventData.ClientIp), eventData.AuthKeyId);
        return Task.CompletedTask;
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
}