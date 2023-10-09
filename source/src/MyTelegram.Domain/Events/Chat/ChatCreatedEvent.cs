namespace MyTelegram.Domain.Events.Chat;

public class ChatCreatedEvent : RequestAggregateEvent2<ChatAggregate, ChatId>
{
    public ChatCreatedEvent(
        RequestInfo requestInfo,
        long chatId,
        long creatorUid,
        string title,
        IReadOnlyList<ChatMember> memberUidList,
        int date,
        long randomId,
        string messageActionData, int? ttlPeriod) : base(requestInfo)
    {
        ChatId = chatId;
        CreatorUid = creatorUid;
        Title = title;
        MemberUidList = memberUidList;
        Date = date;
        RandomId = randomId;
        MessageActionData = messageActionData;
        TtlPeriod = ttlPeriod;
    }

    public long ChatId { get; }
    public long CreatorUid { get; }
    public int Date { get; }
    public IReadOnlyList<ChatMember> MemberUidList { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }
    public string Title { get; }
    public int? TtlPeriod { get; }
}