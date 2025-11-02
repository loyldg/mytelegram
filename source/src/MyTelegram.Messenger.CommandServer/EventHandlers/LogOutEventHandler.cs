using MyTelegram.Domain.Aggregates.Device;
using MyTelegram.Domain.Commands.Device;

namespace MyTelegram.Messenger.CommandServer.EventHandlers;

public class LogOutEventHandler(
        ICommandBus commandBus
    ) : IEventHandler<UserLoggedOutEvent>,
        ITransientDependency
{
    public async Task HandleEventAsync(UserLoggedOutEvent eventData)
    {
        var command = new UnRegisterDeviceForAuthKeyCommand(
                    DeviceId.Create(eventData.PermAuthKeyId),
                    eventData.PermAuthKeyId,
                    eventData.TempAuthKeyId);
        await commandBus.PublishAsync(command, CancellationToken.None);
    }
}
