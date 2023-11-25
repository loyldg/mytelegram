// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Key derivation function to use when generating the <a href="https://corefork.telegram.org/api/srp">password hash for SRP two-factor authorization</a>
/// See <a href="https://corefork.telegram.org/constructor/PasswordKdfAlgo" />
///</summary>
[JsonDerivedType(typeof(TPasswordKdfAlgoUnknown), nameof(TPasswordKdfAlgoUnknown))]
[JsonDerivedType(typeof(TPasswordKdfAlgoSHA256SHA256PBKDF2HMACSHA512iter100000SHA256ModPow), nameof(TPasswordKdfAlgoSHA256SHA256PBKDF2HMACSHA512iter100000SHA256ModPow))]
public interface IPasswordKdfAlgo : IObject
{

}
