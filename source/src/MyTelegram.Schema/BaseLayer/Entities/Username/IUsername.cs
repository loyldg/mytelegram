// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains information about a username
/// See <a href="https://corefork.telegram.org/constructor/Username" />
///</summary>
public interface IUsername : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the username is editable, meaning it wasn't bought on <a href="https://fragment.com/">fragment</a>.
    ///</summary>
    bool Editable { get; set; }

    ///<summary>
    /// Whether the username is active.
    ///</summary>
    bool Active { get; set; }

    ///<summary>
    /// The username.
    ///</summary>
    string Username { get; set; }
}
