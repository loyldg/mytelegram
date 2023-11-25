// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Settings for the code type to send
/// See <a href="https://corefork.telegram.org/constructor/CodeSettings" />
///</summary>
[JsonDerivedType(typeof(TCodeSettings), nameof(TCodeSettings))]
public interface ICodeSettings : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether to allow phone verification via <a href="https://corefork.telegram.org/api/auth">phone calls</a>.
    ///</summary>
    bool AllowFlashcall { get; set; }

    ///<summary>
    /// Pass true if the phone number is used on the current device. Ignored if allow_flashcall is not set.
    ///</summary>
    bool CurrentNumber { get; set; }

    ///<summary>
    /// If a token that will be included in eventually sent SMSs is required: required in newer versions of android, to use the <a href="https://developers.google.com/identity/sms-retriever/overview">android SMS receiver APIs</a>
    ///</summary>
    bool AllowAppHash { get; set; }

    ///<summary>
    /// Whether this device supports receiving the code using the <a href="https://corefork.telegram.org/constructor/auth.codeTypeMissedCall">auth.codeTypeMissedCall</a> method
    ///</summary>
    bool AllowMissedCall { get; set; }

    ///<summary>
    /// Whether Firebase auth is supported
    ///</summary>
    bool AllowFirebase { get; set; }

    ///<summary>
    /// Previously stored future auth tokens, see <a href="https://corefork.telegram.org/api/auth#future-auth-tokens">the documentation for more info »</a>
    ///</summary>
    TVector<byte[]>? LogoutTokens { get; set; }

    ///<summary>
    /// Used only by official iOS apps for Firebase auth: device token for apple push.
    ///</summary>
    string? Token { get; set; }

    ///<summary>
    /// Used only by official iOS apps for firebase auth: whether a sandbox-certificate will be used during transmission of the push notification.
    /// See <a href="https://corefork.telegram.org/type/Bool" />
    ///</summary>
    bool? AppSandbox { get; set; }
}
