// ReSharper disable All

namespace MyTelegram.Schema.Auth;

///<summary>
/// <a href="https://corefork.telegram.org/api/auth#future-auth-tokens">Future auth token »</a> to be used on subsequent authorizations
/// See <a href="https://corefork.telegram.org/constructor/auth.LoggedOut" />
///</summary>
public interface ILoggedOut : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/auth#future-auth-tokens">Future auth token »</a> to be used on subsequent authorizations
    ///</summary>
    byte[]? FutureAuthToken { get; set; }
}
