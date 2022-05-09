namespace MyTelegram.Domain.CommandHandlers.Device;

public class
    UnRegisterDeviceForAuthKeyCommandHandler : CommandHandler<DeviceAggregate, DeviceId,
        UnRegisterDeviceForAuthKeyCommand>
{
    public override Task ExecuteAsync(DeviceAggregate aggregate,
        UnRegisterDeviceForAuthKeyCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UnRegisterDevice(command.PermAuthKeyId, command.TempAuthKeyId);
        return Task.CompletedTask;
    }
}
