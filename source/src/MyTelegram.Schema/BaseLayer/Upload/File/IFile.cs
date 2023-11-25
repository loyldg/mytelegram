// ReSharper disable All

namespace MyTelegram.Schema.Upload;

///<summary>
/// Contains info on file.
/// See <a href="https://corefork.telegram.org/constructor/upload.File" />
///</summary>
[JsonDerivedType(typeof(TFile), nameof(TFile))]
[JsonDerivedType(typeof(TFileCdnRedirect), nameof(TFileCdnRedirect))]
public interface IFile : IObject
{

}
