// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// KDF algorithm to use for computing telegram <a href="https://corefork.telegram.org/passport">passport</a> hash
/// See <a href="https://corefork.telegram.org/constructor/SecurePasswordKdfAlgo" />
///</summary>
[JsonDerivedType(typeof(TSecurePasswordKdfAlgoUnknown), nameof(TSecurePasswordKdfAlgoUnknown))]
[JsonDerivedType(typeof(TSecurePasswordKdfAlgoPBKDF2HMACSHA512iter100000), nameof(TSecurePasswordKdfAlgoPBKDF2HMACSHA512iter100000))]
[JsonDerivedType(typeof(TSecurePasswordKdfAlgoSHA512), nameof(TSecurePasswordKdfAlgoSHA512))]
public interface ISecurePasswordKdfAlgo : IObject
{

}
