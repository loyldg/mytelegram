namespace MyTelegram.MessengerServer.Services.Impl;

public class SessionMessageDataProcessor : IDataProcessor<ISessionMessage>
{
    private readonly IEventBus _eventBus;

    public SessionMessageDataProcessor(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task ProcessAsync(ISessionMessage data)
    {
        switch (data)
        {
            case DataResultResponseReceivedEvent dataResultResponseReceivedEvent:
                await _eventBus.PublishAsync(dataResultResponseReceivedEvent).ConfigureAwait(false);
                break;
            case FileDataResultResponseReceivedEvent fileDataResultResponseReceivedEvent:
                await _eventBus.PublishAsync(fileDataResultResponseReceivedEvent).ConfigureAwait(false);
                break;
            case PushMessageToPeerEvent pushMessageToPeerEvent:
                await _eventBus.PublishAsync(pushMessageToPeerEvent).ConfigureAwait(false);
                break;
            case PushSessionMessageToAuthKeyIdEvent pushSessionMessageToAuthKeyIdEvent:
                await _eventBus.PublishAsync(pushSessionMessageToAuthKeyIdEvent).ConfigureAwait(false);
                break;
            case PushSessionMessageToPeerEvent pushSessionMessageToPeerEvent:
                await _eventBus.PublishAsync(pushSessionMessageToPeerEvent).ConfigureAwait(false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(data));
        }
    }
}