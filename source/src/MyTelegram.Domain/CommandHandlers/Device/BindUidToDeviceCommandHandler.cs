namespace MyTelegram.Domain.CommandHandlers.Device;

public class BindUidToDeviceCommandHandler : CommandHandler<DeviceAggregate, DeviceId, BindUidToDeviceCommand>
{
    public override Task ExecuteAsync(DeviceAggregate aggregate,
        BindUidToDeviceCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.BindUidToDevice(command.UserId, command.PermAuthKeyId);
        return Task.CompletedTask;
    }
}
