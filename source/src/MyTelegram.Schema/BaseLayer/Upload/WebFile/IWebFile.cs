// ReSharper disable All

namespace MyTelegram.Schema.Upload;

///<summary>
/// Remote file
/// See <a href="https://corefork.telegram.org/constructor/upload.WebFile" />
///</summary>
[JsonDerivedType(typeof(TWebFile), nameof(TWebFile))]
public interface IWebFile : IObject
{
    ///<summary>
    /// File size
    ///</summary>
    int Size { get; set; }

    ///<summary>
    /// Mime type
    ///</summary>
    string MimeType { get; set; }

    ///<summary>
    /// File type
    /// See <a href="https://corefork.telegram.org/type/storage.FileType" />
    ///</summary>
    MyTelegram.Schema.Storage.IFileType FileType { get; set; }

    ///<summary>
    /// Modified time
    ///</summary>
    int Mtime { get; set; }

    ///<summary>
    /// Data
    ///</summary>
    byte[] Bytes { get; set; }
}
