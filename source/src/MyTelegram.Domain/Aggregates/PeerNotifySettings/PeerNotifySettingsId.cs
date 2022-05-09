namespace MyTelegram.Domain.Aggregates.PeerNotifySettings;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<PeerNotifySettingsId>))]
public class PeerNotifySettingsId : MyIdentity<PeerNotifySettingsId>
{
    public PeerNotifySettingsId(string value) : base(value)
    {
    }

    public static PeerNotifySettingsId Create(long userId,
        PeerType toPeerType,
        long toPeerId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"PeerNotifySettings_{userId}_{toPeerType}_{toPeerId}");
    }
}
