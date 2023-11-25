// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Defines the location of a file for download.
/// See <a href="https://corefork.telegram.org/constructor/InputFileLocation" />
///</summary>
[JsonDerivedType(typeof(TInputFileLocation), nameof(TInputFileLocation))]
[JsonDerivedType(typeof(TInputEncryptedFileLocation), nameof(TInputEncryptedFileLocation))]
[JsonDerivedType(typeof(TInputDocumentFileLocation), nameof(TInputDocumentFileLocation))]
[JsonDerivedType(typeof(TInputSecureFileLocation), nameof(TInputSecureFileLocation))]
[JsonDerivedType(typeof(TInputTakeoutFileLocation), nameof(TInputTakeoutFileLocation))]
[JsonDerivedType(typeof(TInputPhotoFileLocation), nameof(TInputPhotoFileLocation))]
[JsonDerivedType(typeof(TInputPhotoLegacyFileLocation), nameof(TInputPhotoLegacyFileLocation))]
[JsonDerivedType(typeof(TInputPeerPhotoFileLocation), nameof(TInputPeerPhotoFileLocation))]
[JsonDerivedType(typeof(TInputStickerSetThumb), nameof(TInputStickerSetThumb))]
[JsonDerivedType(typeof(TInputGroupCallStream), nameof(TInputGroupCallStream))]
public interface IInputFileLocation : IObject
{

}
