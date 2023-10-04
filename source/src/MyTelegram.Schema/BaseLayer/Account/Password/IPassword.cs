// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Configuration for two-factor authorization
/// See <a href="https://corefork.telegram.org/constructor/account.Password" />
///</summary>
public interface IPassword : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the user has a recovery method configured
    ///</summary>
    bool HasRecovery { get; set; }

    ///<summary>
    /// Whether telegram <a href="https://corefork.telegram.org/passport">passport</a> is enabled
    ///</summary>
    bool HasSecureValues { get; set; }

    ///<summary>
    /// Whether the user has a password
    ///</summary>
    bool HasPassword { get; set; }

    ///<summary>
    /// The <a href="https://corefork.telegram.org/api/srp">KDF algorithm for SRP two-factor authentication</a> of the current password
    /// See <a href="https://corefork.telegram.org/type/PasswordKdfAlgo" />
    ///</summary>
    MyTelegram.Schema.IPasswordKdfAlgo? CurrentAlgo { get; set; }

    ///<summary>
    /// Srp B param for <a href="https://corefork.telegram.org/api/srp">SRP authorization</a>
    ///</summary>
    byte[]? SrpB { get; set; }

    ///<summary>
    /// Srp ID param for <a href="https://corefork.telegram.org/api/srp">SRP authorization</a>
    ///</summary>
    long? SrpId { get; set; }

    ///<summary>
    /// Text hint for the password
    ///</summary>
    string? Hint { get; set; }

    ///<summary>
    /// A <a href="https://corefork.telegram.org/api/srp#email-verification">password recovery email</a> with the specified <a href="https://corefork.telegram.org/api/pattern">pattern</a> is still awaiting verification
    ///</summary>
    string? EmailUnconfirmedPattern { get; set; }

    ///<summary>
    /// The <a href="https://corefork.telegram.org/api/srp">KDF algorithm for SRP two-factor authentication</a> to use when creating new passwords
    /// See <a href="https://corefork.telegram.org/type/PasswordKdfAlgo" />
    ///</summary>
    MyTelegram.Schema.IPasswordKdfAlgo NewAlgo { get; set; }

    ///<summary>
    /// The KDF algorithm for telegram <a href="https://corefork.telegram.org/passport">passport</a>
    /// See <a href="https://corefork.telegram.org/type/SecurePasswordKdfAlgo" />
    ///</summary>
    MyTelegram.Schema.ISecurePasswordKdfAlgo NewSecureAlgo { get; set; }

    ///<summary>
    /// Secure random string
    ///</summary>
    byte[] SecureRandom { get; set; }

    ///<summary>
    /// The 2FA password will be automatically removed at this date, unless the user cancels the operation
    ///</summary>
    int? PendingResetDate { get; set; }

    ///<summary>
    /// A verified login email with the specified <a href="https://corefork.telegram.org/api/pattern">pattern</a> is configured
    ///</summary>
    string? LoginEmailPattern { get; set; }
}
