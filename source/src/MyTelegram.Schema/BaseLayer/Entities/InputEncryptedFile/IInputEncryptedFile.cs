// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object sets encrypted file for attachment
/// See <a href="https://corefork.telegram.org/constructor/InputEncryptedFile" />
///</summary>
[JsonDerivedType(typeof(TInputEncryptedFileEmpty), nameof(TInputEncryptedFileEmpty))]
[JsonDerivedType(typeof(TInputEncryptedFileUploaded), nameof(TInputEncryptedFileUploaded))]
[JsonDerivedType(typeof(TInputEncryptedFile), nameof(TInputEncryptedFile))]
[JsonDerivedType(typeof(TInputEncryptedFileBigUploaded), nameof(TInputEncryptedFileBigUploaded))]
public interface IInputEncryptedFile : IObject
{

}
