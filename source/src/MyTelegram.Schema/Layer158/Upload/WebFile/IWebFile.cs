// ReSharper disable All

using MyTelegram.Schema.Storage;

namespace MyTelegram.Schema.Upload;

public interface IWebFile : IObject
{
    int Size { get; set; }
    string MimeType { get; set; }
    IFileType FileType { get; set; }
    int Mtime { get; set; }
    byte[] Bytes { get; set; }
}
