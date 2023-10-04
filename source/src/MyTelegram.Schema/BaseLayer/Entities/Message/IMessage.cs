// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object describing a message.
/// See <a href="https://corefork.telegram.org/constructor/Message" />
///</summary>
public interface IMessage : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Message ID
    ///</summary>
    int Id { get; set; }
}
