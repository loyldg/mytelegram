// ReSharper disable All

namespace MyTelegram.Schema;

public interface ILayeredMessage : IMessage
{
    IPeer? FromId { get; set; }
    bool Out { get; set; }
    //IMessageReplyHeader? ReplyTo { get; set; }
    bool Mentioned { get; set; }
    bool MediaUnread { get; set; }
    int? QuickReplyShortcutId { get; set; }
    IMessageFwdHeader? FwdFrom { get; set; }
    MyTelegram.Schema.IMessageReplies? Replies { get; set; }
    MyTelegram.Schema.IMessageReactions? Reactions { get; set; }
    int Date { get; set; }
    bool FromScheduled { get; set; }
    IMessageMedia? Media { get; set; }

    ///<summary>
    /// Message <a href="https://corefork.telegram.org/api/entities">entities</a> for styled text
    ///</summary>
    TVector<MyTelegram.Schema.IMessageEntity>? Entities { get; set; }
}