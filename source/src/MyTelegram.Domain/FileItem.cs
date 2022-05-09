namespace MyTelegram.Domain;

public class FileItem
{
    public long AccessHash { get; set; }
    public byte[]? Attributes { get; set; }
    public int Date { get; set; }
    public long FileId { get; set; }
    public Guid FileReference { get; set; }
    public int LocalId { get; set; }
    public string? Md5 { get; set; }
    public bool Merged { get; set; }
    public string? MimeType { get; set; }

    public string? Name { get; set; }

    //public List<FilePart> Parts { get; set; }
    public HashSet<int> Parts { get; set; } = new();
    public long ServerFileId { get; set; }
    public int Size { get; set; }
    public byte[]? Thumbs { get; set; }
    public int TotalParts { get; set; }
    public long UserId { get; set; }
    public long VolumeId { get; set; }
}
