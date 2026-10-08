namespace MyTelegram;

public record DocumentItem(
    long Id,
    long AccessHash,
    int DcId,
    int Date,
    string MimeType,
    long Size,
    ReadOnlyMemory<byte> FileReference,
    string? Name = null,
    long? CreatorId = null,
    long? ThumbId = null,
    long? VideoThumbId = null,
    string? Md5CheckSum = null,
    //byte[]? Attributes = null,
    //byte[]? Stickers = null,
    List<PhotoSize>? Thumbs = null,
    List<VideoSize>? VideoThumbs = null,
    int? Fingerprint = null,
    List<IDocumentAttribute>? Attributes2 = null);