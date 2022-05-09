// ReSharper disable All

namespace MyTelegram.Schema.Upload;

public interface IWebFile : IObject
{
    int Size { get; set; }
    string MimeType { get; set; }
    MyTelegram.Schema.Storage.IFileType FileType { get; set; }
    int Mtime { get; set; }
    byte[] Bytes { get; set; }

}
