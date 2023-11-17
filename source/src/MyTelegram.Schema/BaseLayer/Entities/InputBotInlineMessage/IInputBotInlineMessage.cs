// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a sent inline message from the perspective of a bot
/// See <a href="https://corefork.telegram.org/constructor/InputBotInlineMessage" />
///</summary>
public interface IInputBotInlineMessage : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.IReplyMarkup? ReplyMarkup { get; set; }
}
