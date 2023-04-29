// ReSharper disable All

namespace MyTelegram.Schema;

public interface IBotInlineMessage : IObject
{
    BitArray Flags { get; set; }
    Schema.IReplyMarkup? ReplyMarkup { get; set; }
}
