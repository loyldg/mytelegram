// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a logged-in session
/// See <a href="https://corefork.telegram.org/constructor/Authorization" />
///</summary>
[JsonDerivedType(typeof(TAuthorization), nameof(TAuthorization))]
public interface IAuthorization : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether this is the current session
    ///</summary>
    bool Current { get; set; }

    ///<summary>
    /// Whether the session is from an official app
    ///</summary>
    bool OfficialApp { get; set; }

    ///<summary>
    /// Whether the session is still waiting for a 2FA password
    ///</summary>
    bool PasswordPending { get; set; }

    ///<summary>
    /// Whether this session will accept encrypted chats
    ///</summary>
    bool EncryptedRequestsDisabled { get; set; }

    ///<summary>
    /// Whether this session will accept phone calls
    ///</summary>
    bool CallRequestsDisabled { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool Unconfirmed { get; set; }

    ///<summary>
    /// Identifier
    ///</summary>
    long Hash { get; set; }

    ///<summary>
    /// Device model
    ///</summary>
    string DeviceModel { get; set; }

    ///<summary>
    /// Platform
    ///</summary>
    string Platform { get; set; }

    ///<summary>
    /// System version
    ///</summary>
    string SystemVersion { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/obtaining_api_id">API ID</a>
    ///</summary>
    int ApiId { get; set; }

    ///<summary>
    /// App name
    ///</summary>
    string AppName { get; set; }

    ///<summary>
    /// App version
    ///</summary>
    string AppVersion { get; set; }

    ///<summary>
    /// When was the session created
    ///</summary>
    int DateCreated { get; set; }

    ///<summary>
    /// When was the session last active
    ///</summary>
    int DateActive { get; set; }

    ///<summary>
    /// Last known IP
    ///</summary>
    string Ip { get; set; }

    ///<summary>
    /// Country determined from IP
    ///</summary>
    string Country { get; set; }

    ///<summary>
    /// Region determined from IP
    ///</summary>
    string Region { get; set; }
}
