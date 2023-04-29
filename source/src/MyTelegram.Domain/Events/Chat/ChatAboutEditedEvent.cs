namespace MyTelegram.Domain.Events.Chat;

public class ChatAboutEditedEvent : RequestAggregateEvent<ChatAggregate, ChatId>
{
    public ChatAboutEditedEvent(long reqMsgId,
        string? about) : base(reqMsgId)
    {
        About = about;
    }

    public string? About { get; }
}

public class CheckChatStateCompletedEvent : AggregateEvent<ChatAggregate, ChatId>, IHasCorrelationId
{
    public CheckChatStateCompletedEvent(
        string title,
        IReadOnlyList<long> memberUidList,
        Guid correlationId)
    {
        Title = title;
        MemberUidList = memberUidList;
        CorrelationId = correlationId;
    }

    public string Title { get; }
    public IReadOnlyList<long> MemberUidList { get; }

    public Guid CorrelationId { get; }
}
