// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Telegram <a href="https://corefork.telegram.org/passport">passport</a> settings
/// See <a href="https://corefork.telegram.org/constructor/SecureSecretSettings" />
///</summary>
[JsonDerivedType(typeof(TSecureSecretSettings), nameof(TSecureSecretSettings))]
public interface ISecureSecretSettings : IObject
{
    ///<summary>
    /// Secure KDF algo
    /// See <a href="https://corefork.telegram.org/type/SecurePasswordKdfAlgo" />
    ///</summary>
    MyTelegram.Schema.ISecurePasswordKdfAlgo SecureAlgo { get; set; }

    ///<summary>
    /// Secure secret
    ///</summary>
    byte[] SecureSecret { get; set; }

    ///<summary>
    /// Secret ID
    ///</summary>
    long SecureSecretId { get; set; }
}
