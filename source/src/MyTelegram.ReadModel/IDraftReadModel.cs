namespace MyTelegram.ReadModel;

public interface IDraftReadModel : IReadModel
{
    Draft Draft { get; }
    string Id { get; }
    long OwnerPeerId { get; }
    Peer Peer { get; }
}