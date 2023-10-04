// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a message <a href="https://corefork.telegram.org/api/drafts">draft</a>.
/// See <a href="https://corefork.telegram.org/constructor/DraftMessage" />
///</summary>
public interface IDraftMessage : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }
}
