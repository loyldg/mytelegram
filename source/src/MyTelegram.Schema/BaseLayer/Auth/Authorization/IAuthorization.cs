// ReSharper disable All

namespace MyTelegram.Schema.Auth;

///<summary>
/// Object contains info on user authorization.
/// See <a href="https://corefork.telegram.org/constructor/auth.Authorization" />
///</summary>
public interface IAuthorization : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }
}
