// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Key derivation function to use when generating the <a href="https://corefork.telegram.org/api/srp">password hash for SRP two-factor authorization</a>
/// See <a href="https://corefork.telegram.org/constructor/PasswordKdfAlgo" />
///</summary>
public interface IPasswordKdfAlgo : IObject
{

}
