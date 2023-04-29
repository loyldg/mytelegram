// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputBotInlineMessage : IObject
{
    BitArray Flags { get; set; }
    Schema.IReplyMarkup? ReplyMarkup { get; set; }
}
