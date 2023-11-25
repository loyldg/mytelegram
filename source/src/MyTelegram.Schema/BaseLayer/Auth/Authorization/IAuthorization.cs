// ReSharper disable All

namespace MyTelegram.Schema.Auth;

///<summary>
/// Object contains info on user authorization.
/// See <a href="https://corefork.telegram.org/constructor/auth.Authorization" />
///</summary>
[JsonDerivedType(typeof(TAuthorization), nameof(TAuthorization))]
[JsonDerivedType(typeof(TAuthorizationSignUpRequired), nameof(TAuthorizationSignUpRequired))]
public interface IAuthorization : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }
}
