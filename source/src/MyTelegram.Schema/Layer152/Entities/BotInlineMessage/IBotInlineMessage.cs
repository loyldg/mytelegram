// ReSharper disable All

namespace MyTelegram.Schema;

public interface IBotInlineMessage : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.IReplyMarkup? ReplyMarkup { get; set; }
}
