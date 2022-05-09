namespace MyTelegram.ReadModel;

public interface IPeerNotifySettingsReadModel : IReadModel
{
    string Id { get; }
    PeerNotifySettings NotifySettings { get; }
    long OwnerPeerId { get; }
    long PeerId { get; }
    PeerType PeerType { get; }
}