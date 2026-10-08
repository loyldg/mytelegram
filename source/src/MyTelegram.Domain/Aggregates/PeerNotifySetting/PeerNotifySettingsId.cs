namespace MyTelegram.Domain.Aggregates.PeerNotifySetting;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<PeerNotifySettingsId>))]
public class PeerNotifySettingsId(string value) : Identity<PeerNotifySettingsId>(value)
{
    public static PeerNotifySettingsId Create(long userId,
        PeerType toPeerType,
        long toPeerId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"PeerNotifySettings_{userId}_{toPeerType}_{toPeerId}");
    }

    public static PeerNotifySettingsId Create2(long userId,
        PeerNotifyType peerNotifyType,
        long toPeerId
        )
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"PeerNotifySettings-{userId}-{peerNotifyType}-{toPeerId}");
    }
}
