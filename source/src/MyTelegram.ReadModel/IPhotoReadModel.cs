namespace MyTelegram.ReadModel;

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