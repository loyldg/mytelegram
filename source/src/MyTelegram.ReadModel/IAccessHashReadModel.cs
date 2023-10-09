namespace MyTelegram.ReadModel;

public interface IAccessHashReadModel : IReadModel
{
    long AccessId { get; }
    long AccessHash { get; }
    AccessHashType AccessHashType { get; }
}

public interface IPeerSettingsReadModel : IReadModel
{
    string Id { get; }
    long OwnerPeerId { get; }
    long PeerId { get; }
    PeerSettings? PeerSettings { get; }
    bool HiddenPeerSettingsBar { get; }
}

public interface IBotReadModel : IReadModel
{
    
}
