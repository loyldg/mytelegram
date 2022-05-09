// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputBotInlineMessage : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.IReplyMarkup? ReplyMarkup { get; set; }

}
