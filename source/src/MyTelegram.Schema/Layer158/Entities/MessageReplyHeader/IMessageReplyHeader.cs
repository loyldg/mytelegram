// ReSharper disable All

namespace MyTelegram.Schema;

public interface IMessageReplyHeader : IObject
{
    BitArray Flags { get; set; }
    bool ReplyToScheduled { get; set; }
    bool ForumTopic { get; set; }
    int ReplyToMsgId { get; set; }
    Schema.IPeer? ReplyToPeerId { get; set; }
    int? ReplyToTopId { get; set; }
}
