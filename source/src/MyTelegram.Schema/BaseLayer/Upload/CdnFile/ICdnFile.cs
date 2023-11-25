// ReSharper disable All

namespace MyTelegram.Schema.Upload;

///<summary>
/// Represents the download status of a CDN file
/// See <a href="https://corefork.telegram.org/constructor/upload.CdnFile" />
///</summary>
[JsonDerivedType(typeof(TCdnFileReuploadNeeded), nameof(TCdnFileReuploadNeeded))]
[JsonDerivedType(typeof(TCdnFile), nameof(TCdnFile))]
public interface ICdnFile : IObject
{

}
