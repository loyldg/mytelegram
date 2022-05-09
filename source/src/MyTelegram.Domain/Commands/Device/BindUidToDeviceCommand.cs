namespace MyTelegram.Domain.Commands.Device;

public class BindUidToDeviceCommand : Command<DeviceAggregate, DeviceId, IExecutionResult>
{
    public BindUidToDeviceCommand(DeviceId aggregateId,
        long userId,
        long permAuthKeyId) : base(aggregateId)
    {
        UserId = userId;
        PermAuthKeyId = permAuthKeyId;
    }

    public long PermAuthKeyId { get; }

    public long UserId { get; }
}
