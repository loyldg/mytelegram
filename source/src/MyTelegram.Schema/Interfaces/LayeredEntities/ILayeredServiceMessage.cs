// ReSharper disable All

namespace MyTelegram.Schema;

public interface ILayeredServiceMessage : IMessage
{
    IPeer? FromId { get; set; }
    bool Out { get; set; }
    //IMessageReplyHeader? ReplyTo { get; set; }
    bool Mentioned { get; set; }
    bool MediaUnread { get; set; }
    MyTelegram.Schema.IMessageReactions? Reactions { get; set; }
    MyTelegram.Schema.IMessageAction Action { get; set; }
}