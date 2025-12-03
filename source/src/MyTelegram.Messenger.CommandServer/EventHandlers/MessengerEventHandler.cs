using MyTelegram.Domain.Aggregates.Device;

namespace MyTelegram.Messenger.CommandServer.EventHandlers;

public class MessengerEventHandler(
    ICommandBus commandBus,
    IMessageQueueProcessor<MessengerCommandDataReceivedEvent> processor,
    IObjectMessageSender objectMessageSender,
    IMessageQueueProcessor<NewDeviceCreatedEvent> newDeviceCreatedProcessor)
    :
        IEventHandler<MessengerCommandDataReceivedEvent>,
        IEventHandler<NewDeviceCreatedEvent>,
        IEventHandler<BindUserIdToAuthKeyIntegrationEvent>,
        IEventHandler<AuthKeyUnRegisteredIntegrationEvent>, ITransientDependency
{
    public Task HandleEventAsync(AuthKeyUnRegisteredIntegrationEvent eventData)
    {
        var command = new UnRegisterDeviceForAuthKeyCommand(DeviceId.Create(eventData.PermAuthKeyId),
            eventData.PermAuthKeyId,
            eventData.TempAuthKeyId);
        return commandBus.PublishAsync(command);
    }

    public Task HandleEventAsync(BindUserIdToAuthKeyIntegrationEvent eventData)
    {
        var command = new BindUserIdToDeviceCommand(DeviceId.Create(eventData.PermAuthKeyId),
            eventData.UserId,
            eventData.PermAuthKeyId);
        return commandBus.PublishAsync(command);
    }

    public Task HandleEventAsync(MessengerCommandDataReceivedEvent eventData)
    {
        processor.Enqueue(eventData, eventData.PermAuthKeyId);
        return Task.CompletedTask;
    }

    public Task HandleEventAsync(NewDeviceCreatedEvent eventData)
    {
        newDeviceCreatedProcessor.Enqueue(eventData, eventData.PermAuthKeyId);
        return Task.CompletedTask;
    }

    public Task HandleEventAsync(UserIsOnlineEvent eventData)
    {
        var updatesTooLong = new TUpdatesTooLong();
        return objectMessageSender.PushSessionMessageToAuthKeyIdAsync(eventData.TempAuthKeyId, updatesTooLong);
    }
}
