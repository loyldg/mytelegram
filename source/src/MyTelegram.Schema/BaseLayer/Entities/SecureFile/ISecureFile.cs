// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Secure <a href="https://corefork.telegram.org/passport">passport</a> file, for more info <a href="https://corefork.telegram.org/passport/encryption#inputsecurefile">see the passport docs »</a>
/// See <a href="https://corefork.telegram.org/constructor/SecureFile" />
///</summary>
[JsonDerivedType(typeof(TSecureFileEmpty), nameof(TSecureFileEmpty))]
[JsonDerivedType(typeof(TSecureFile), nameof(TSecureFile))]
public interface ISecureFile : IObject
{

}
