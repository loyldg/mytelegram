namespace MyTelegram.Domain.Events.Chat;

public class ChatCreatedEvent : RequestAggregateEvent2<ChatAggregate, ChatId>, IHasCorrelationId
{
    public ChatCreatedEvent(
        RequestInfo request,
        long chatId,
        long creatorUid,
        string title,
        IReadOnlyList<ChatMember> memberUidList,
        int date,
        long randomId,
        string messageActionData,
        Guid correlationId) : base(request)
    {
        ChatId = chatId;
        CreatorUid = creatorUid;
        Title = title;
        MemberUidList = memberUidList;
        Date = date;
        RandomId = randomId;
        MessageActionData = messageActionData;
        CorrelationId = correlationId;
    }

    public long ChatId { get; }
    public long CreatorUid { get; }
    public int Date { get; }
    public IReadOnlyList<ChatMember> MemberUidList { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }
    public string Title { get; }

    public Guid CorrelationId { get; }
}
