namespace MyTelegram.Domain.Aggregates.PeerNotifySettings;

public class PeerNotifySettingsSnapshot : ISnapshot
{
    public PeerNotifySettingsSnapshot(ValueObjects.PeerNotifySettings peerNotifySettings)
    {
        PeerNotifySettings = peerNotifySettings;
    }

    public ValueObjects.PeerNotifySettings PeerNotifySettings { get; }
}
