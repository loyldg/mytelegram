// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Inline message
/// See <a href="https://corefork.telegram.org/constructor/BotInlineMessage" />
///</summary>
public interface IBotInlineMessage : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.IReplyMarkup? ReplyMarkup { get; set; }
}
