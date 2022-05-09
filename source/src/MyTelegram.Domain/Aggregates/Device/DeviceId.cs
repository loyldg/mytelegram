namespace MyTelegram.Domain.Aggregates.Device;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<DeviceId>))]
public class DeviceId : MyIdentity<DeviceId>
{
    public DeviceId(string value) : base(value)
    {
    }

    public static DeviceId Create(long permAuthKeyId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"device_{permAuthKeyId}");
    }
}
