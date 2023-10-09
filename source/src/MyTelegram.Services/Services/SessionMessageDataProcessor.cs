using MyTelegram.Core;

namespace MyTelegram.Services.Services;

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
                await _eventBus.PublishAsync(dataResultResponseReceivedEvent);
                break;
            case DataResultResponseWithUserIdReceivedEvent dataResultResponseWithUserIdReceivedEvent:
                await _eventBus.PublishAsync(dataResultResponseWithUserIdReceivedEvent);
                break;
            case FileDataResultResponseReceivedEvent fileDataResultResponseReceivedEvent:
                await _eventBus.PublishAsync(fileDataResultResponseReceivedEvent);
                break;
            case LayeredAuthKeyIdMessageCreatedIntegrationEvent layeredAuthKeyIdMessageCreatedIntegrationEvent:
                await _eventBus.PublishAsync(layeredAuthKeyIdMessageCreatedIntegrationEvent);
                break;
            case LayeredPushMessageCreatedIntegrationEvent layeredPushMessageCreatedIntegrationEvent:
                await _eventBus.PublishAsync(layeredPushMessageCreatedIntegrationEvent);
                break;
            case PushMessageToPeerEvent pushMessageToPeerEvent:
                await _eventBus.PublishAsync(pushMessageToPeerEvent);
                break;
            //case PushSessionMessageToAuthKeyIdEvent pushSessionMessageToAuthKeyIdEvent:
            //    await _eventBus.PublishAsync(pushSessionMessageToAuthKeyIdEvent);
            //    break;
            //case PushSessionMessageToPeerEvent pushSessionMessageToPeerEvent:
            //    await _eventBus.PublishAsync(pushSessionMessageToPeerEvent);
            //    break;


            default:
                throw new ArgumentOutOfRangeException(nameof(data));
        }

        //return Task.CompletedTask;
    }
}