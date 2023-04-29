namespace MyTelegram.ReadModel;

public interface IPtsForAuthKeyIdReadModel : IReadModel
{
    long GlobalSeqNo { get; }
    string Id { get; }
    long PeerId { get; }
    long PermAuthKeyId { get; }
    int Pts { get; }
}
