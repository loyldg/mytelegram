namespace MyTelegram.Domain.Events.Chat;

//public class SendChatMessageStartedEvent:AggregateEvent<chata>

public class StartSendChatMessageEvent : /*RequestInfo*/AggregateEvent<ChatAggregate, ChatId>, IHasCorrelationId
{
    public StartSendChatMessageEvent(
        //long reqMsgId,
        long chatId,
        string title,
        IReadOnlyList<long> memberUidList,
        long senderPeerId,
        int senderMessageId,
        bool senderIsBot,
        long lastDeletedMemberUid,
        Guid correlationId) //: base(reqMsgId)
    {
        ChatId = chatId;
        Title = title;
        MemberUidList = memberUidList;
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        SenderIsBot = senderIsBot;
        LastDeletedMemberUid = lastDeletedMemberUid;
        CorrelationId = correlationId;
    }

    public long ChatId { get; }
    public long LastDeletedMemberUid { get; }
    public IReadOnlyList<long> MemberUidList { get; }
    public bool SenderIsBot { get; }
    public int SenderMessageId { get; }
    public long SenderPeerId { get; }
    public string Title { get; }
    public Guid CorrelationId { get; }
}
