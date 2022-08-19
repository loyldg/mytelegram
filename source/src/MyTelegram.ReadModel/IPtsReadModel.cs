namespace MyTelegram.ReadModel;

public interface IPtsReadModel : IReadModel
{
    int Date { get; }
    long GlobalSeqNo { get; }
    string Id { get; }

    long PeerId { get; }
    int Pts { get; }
    int Qts { get; }
    int UnreadCount { get; }
}   