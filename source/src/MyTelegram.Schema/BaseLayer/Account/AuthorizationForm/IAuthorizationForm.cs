// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Authorization form
/// See <a href="https://corefork.telegram.org/constructor/account.AuthorizationForm" />
///</summary>
[JsonDerivedType(typeof(TAuthorizationForm), nameof(TAuthorizationForm))]
public interface IAuthorizationForm : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Required <a href="https://corefork.telegram.org/passport">Telegram Passport</a> documents
    /// See <a href="https://corefork.telegram.org/type/SecureRequiredType" />
    ///</summary>
    TVector<MyTelegram.Schema.ISecureRequiredType> RequiredTypes { get; set; }

    ///<summary>
    /// Already submitted <a href="https://corefork.telegram.org/passport">Telegram Passport</a> documents
    /// See <a href="https://corefork.telegram.org/type/SecureValue" />
    ///</summary>
    TVector<MyTelegram.Schema.ISecureValue> Values { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/passport">Telegram Passport</a> errors
    /// See <a href="https://corefork.telegram.org/type/SecureValueError" />
    ///</summary>
    TVector<MyTelegram.Schema.ISecureValueError> Errors { get; set; }

    ///<summary>
    /// Info about the bot to which the form will be submitted
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

    ///<summary>
    /// URL of the service's privacy policy
    ///</summary>
    string? PrivacyPolicyUrl { get; set; }
}
