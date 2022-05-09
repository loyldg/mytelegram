namespace MyTelegram.Domain.Events.Device;

public class BindUidToDeviceEvent : AggregateEvent<DeviceAggregate, DeviceId>
{
    public BindUidToDeviceEvent(long userId,
        long permAuthKeyId,
        int date)
    {
        UserId = userId;
        PermAuthKeyId = permAuthKeyId;
        Date = date;
    }

    public int Date { get; }
    public long PermAuthKeyId { get; }

    public long UserId { get; }
}
