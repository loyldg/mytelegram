// ReSharper disable All

namespace MyTelegram.Schema.Storage;

///<summary>
/// Object describes the file type.
/// See <a href="https://corefork.telegram.org/constructor/storage.FileType" />
///</summary>
[JsonDerivedType(typeof(TFileUnknown), nameof(TFileUnknown))]
[JsonDerivedType(typeof(TFilePartial), nameof(TFilePartial))]
[JsonDerivedType(typeof(TFileJpeg), nameof(TFileJpeg))]
[JsonDerivedType(typeof(TFileGif), nameof(TFileGif))]
[JsonDerivedType(typeof(TFilePng), nameof(TFilePng))]
[JsonDerivedType(typeof(TFilePdf), nameof(TFilePdf))]
[JsonDerivedType(typeof(TFileMp3), nameof(TFileMp3))]
[JsonDerivedType(typeof(TFileMov), nameof(TFileMov))]
[JsonDerivedType(typeof(TFileMp4), nameof(TFileMp4))]
[JsonDerivedType(typeof(TFileWebp), nameof(TFileWebp))]
public interface IFileType : IObject
{

}
