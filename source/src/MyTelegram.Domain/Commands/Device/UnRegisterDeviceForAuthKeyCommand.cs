namespace MyTelegram.Domain.Commands.Device;

public class UnRegisterDeviceForAuthKeyCommand : Command<DeviceAggregate, DeviceId, IExecutionResult>
{
    public UnRegisterDeviceForAuthKeyCommand(DeviceId aggregateId,
        long permAuthKeyId,
        long tempAuthKeyId) : base(aggregateId)
    {
        PermAuthKeyId = permAuthKeyId;
        TempAuthKeyId = tempAuthKeyId;
    }

    public long PermAuthKeyId { get; }
    public long TempAuthKeyId { get; }
}
