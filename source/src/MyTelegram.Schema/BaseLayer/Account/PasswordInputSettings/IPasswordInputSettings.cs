// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Constructor for setting up a new <a href="https://corefork.telegram.org/api/srp">2FA SRP password</a>
/// See <a href="https://corefork.telegram.org/constructor/account.PasswordInputSettings" />
///</summary>
[JsonDerivedType(typeof(TPasswordInputSettings), nameof(TPasswordInputSettings))]
public interface IPasswordInputSettings : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// The <a href="https://corefork.telegram.org/api/srp">SRP algorithm</a> to use
    /// See <a href="https://corefork.telegram.org/type/PasswordKdfAlgo" />
    ///</summary>
    MyTelegram.Schema.IPasswordKdfAlgo? NewAlgo { get; set; }

    ///<summary>
    /// The <a href="https://corefork.telegram.org/api/srp">computed password hash</a>
    ///</summary>
    byte[]? NewPasswordHash { get; set; }

    ///<summary>
    /// Text hint for the password
    ///</summary>
    string? Hint { get; set; }

    ///<summary>
    /// Password recovery email
    ///</summary>
    string? Email { get; set; }

    ///<summary>
    /// Telegram <a href="https://corefork.telegram.org/passport">passport</a> settings
    /// See <a href="https://corefork.telegram.org/type/SecureSecretSettings" />
    ///</summary>
    MyTelegram.Schema.ISecureSecretSettings? NewSecureSettings { get; set; }
}
