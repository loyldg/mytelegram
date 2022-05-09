namespace MyTelegram.ReadModel;

public interface IFileReadModel : IReadModel
{
    long AccessHash { get; }
    byte[]? Attributes { get; }
    int Date { get; }
    long FileId { get; }
    Guid FileReference { get; }
    string Id { get; }
    int LocalId { get; }
    string? Md5CheckSum { get; }
    string MimeType { get; }
    string? Name { get; }
    int Parts { get; }
    long ServerFileId { get; }
    int Size { get; }

    byte[]? Thumbs { get; }
    int TotalParts { get; }
    long UserId { get; }
    long VolumeId { get; }
}