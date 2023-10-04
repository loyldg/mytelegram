// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Chat info.
/// See <a href="https://corefork.telegram.org/constructor/Dialog" />
///</summary>
public interface IDialog : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Is this folder pinned
    ///</summary>
    bool Pinned { get; set; }

    ///<summary>
    /// Peer in folder
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer Peer { get; set; }

    ///<summary>
    /// Latest message ID of dialog
    ///</summary>
    int TopMessage { get; set; }
}
