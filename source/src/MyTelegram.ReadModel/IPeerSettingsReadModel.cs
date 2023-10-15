namespace MyTelegram.ReadModel;

public interface IPeerSettingsReadModel : IReadModel
{
    string Id { get; }
    long OwnerPeerId { get; }
    long PeerId { get; }
    PeerSettings? PeerSettings { get; }
    bool HiddenPeerSettingsBar { get; }
}