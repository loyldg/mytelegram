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

public interface IPhotoReadModel : IReadModel
{
    long AccessHash { get; }
    int Date { get; }
    int DcId { get; }
    byte[] FileReference { get; }
    bool HasStickers { get; }
    long PhotoId { get; }
    long Size { get; }
    List<PhotoSize>? Sizes { get; }
    long UserId { get; }
    List<VideoSize>? VideoSizes { get; }
}