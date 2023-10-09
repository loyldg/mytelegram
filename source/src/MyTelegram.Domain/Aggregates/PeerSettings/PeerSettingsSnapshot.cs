namespace MyTelegram.Domain.Aggregates.PeerSettings;

public class PeerSettingsSnapshot : ISnapshot
{

    public PeerSettingsSnapshot(ValueObjects.PeerSettings peerSettings, bool hidePeerSettingsBar, long ownerPeerId, long peerId)
    {
        PeerSettings = peerSettings;
        HidePeerSettingsBar = hidePeerSettingsBar;
        OwnerPeerId = ownerPeerId;
        PeerId = peerId;
    }

    public ValueObjects.PeerSettings PeerSettings { get; }
    public bool HidePeerSettingsBar { get; private set; }
    public long OwnerPeerId { get; private set; }
    public long PeerId { get; private set; }
}