namespace MyTelegram.Domain.Aggregates.Device;

public class DeviceState : AggregateState<DeviceAggregate, DeviceId, DeviceState>,
    IApply<DeviceCreatedEvent>,
    IApply<BindUidToDeviceEvent>,
    IApply<DeviceAuthKeyUnRegisteredEvent>
{
    public long PermAuthKeyId { get; private set; }
    public long UserId { get; private set; }

    public void Apply(BindUidToDeviceEvent aggregateEvent)
    {
        UserId = aggregateEvent.UserId;
    }

    public void Apply(DeviceAuthKeyUnRegisteredEvent aggregateEvent)
    {
    }

    public void Apply(DeviceCreatedEvent aggregateEvent)
    {
        PermAuthKeyId = aggregateEvent.PermAuthKeyId;
    }
}
