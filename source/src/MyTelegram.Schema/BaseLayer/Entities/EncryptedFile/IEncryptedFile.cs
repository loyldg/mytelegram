// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Seta an encrypted file.
/// See <a href="https://corefork.telegram.org/constructor/EncryptedFile" />
///</summary>
[JsonDerivedType(typeof(TEncryptedFileEmpty), nameof(TEncryptedFileEmpty))]
[JsonDerivedType(typeof(TEncryptedFile), nameof(TEncryptedFile))]
public interface IEncryptedFile : IObject
{

}
