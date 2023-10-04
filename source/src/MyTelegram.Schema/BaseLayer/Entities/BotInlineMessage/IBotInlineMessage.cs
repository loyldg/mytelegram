// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Inline message
/// See <a href="https://corefork.telegram.org/constructor/BotInlineMessage" />
///</summary>
public interface IBotInlineMessage : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Inline keyboard
    /// See <a href="https://corefork.telegram.org/type/ReplyMarkup" />
    ///</summary>
    MyTelegram.Schema.IReplyMarkup? ReplyMarkup { get; set; }
}
