namespace MyTelegram.Domain.Events.Device;

public class DeviceAuthKeyUnRegisteredEvent : AggregateEvent<DeviceAggregate, DeviceId>
{
    public DeviceAuthKeyUnRegisteredEvent(long permAuthKeyId,
        long tempAuthKeyId)
    {
        PermAuthKeyId = permAuthKeyId;
        TempAuthKeyId = tempAuthKeyId;
    }

    public long PermAuthKeyId { get; }
    public long TempAuthKeyId { get; }
}
