// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Private info associated to the password info (recovery email, telegram <a href="https://corefork.telegram.org/passport">passport</a> info &amp; so on)
/// See <a href="https://corefork.telegram.org/constructor/account.PasswordSettings" />
///</summary>
[JsonDerivedType(typeof(TPasswordSettings), nameof(TPasswordSettings))]
public interface IPasswordSettings : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/srp#email-verification">2FA Recovery email</a>
    ///</summary>
    string? Email { get; set; }

    ///<summary>
    /// Telegram <a href="https://corefork.telegram.org/passport">passport</a> settings
    /// See <a href="https://corefork.telegram.org/type/SecureSecretSettings" />
    ///</summary>
    MyTelegram.Schema.ISecureSecretSettings? SecureSettings { get; set; }
}
