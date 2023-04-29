// ReSharper disable All

namespace MyTelegram.Schema;

public interface IMessageViews : IObject
{
    BitArray Flags { get; set; }
    int? Views { get; set; }
    int? Forwards { get; set; }
    Schema.IMessageReplies? Replies { get; set; }
}
