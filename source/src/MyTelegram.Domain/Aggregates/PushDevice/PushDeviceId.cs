namespace MyTelegram.Domain.Aggregates.PushDevice;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<PushDeviceId>))]
public class PushDeviceId : MyIdentity<PushDeviceId>
{
    public PushDeviceId(string value) : base(value)
    {
    }

    public static PushDeviceId Create(string token)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"pushdevice_{token}");
    }
}
