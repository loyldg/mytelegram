namespace MyTelegram.Domain.Events.Chat;

//public class SendChatMessageStartedEvent:AggregateEvent<chata>

public class StartSendChatMessageEvent : /*Request*/RequestAggregateEvent2<ChatAggregate, ChatId>
{
    public StartSendChatMessageEvent(
        //long reqMsgId,
        RequestInfo requestInfo,
        long chatId,
        string title,
        IReadOnlyList<long> memberUidList,
        long senderPeerId,
        int senderMessageId,
        bool senderIsBot,
        long lastDeletedMemberUid) : base(requestInfo)
    {
        ChatId = chatId;
        Title = title;
        MemberUidList = memberUidList;
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        SenderIsBot = senderIsBot;
        LastDeletedMemberUid = lastDeletedMemberUid;

    }

    public long ChatId { get; }
    public long LastDeletedMemberUid { get; }
    public IReadOnlyList<long> MemberUidList { get; }
    public bool SenderIsBot { get; }
    public int SenderMessageId { get; }
    public long SenderPeerId { get; }
    public string Title { get; }

}