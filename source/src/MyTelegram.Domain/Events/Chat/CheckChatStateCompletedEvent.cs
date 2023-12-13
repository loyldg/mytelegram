namespace MyTelegram.Domain.Events.Chat;

public class CheckChatStateCompletedEvent : RequestAggregateEvent2<ChatAggregate, ChatId>
{
    public CheckChatStateCompletedEvent(
        RequestInfo requestInfo,
        string title,
        IReadOnlyList<long> memberUidList) : base(requestInfo)
    {
        Title = title;
        MemberUidList = memberUidList;

    }

    public string Title { get; }
    public IReadOnlyList<long> MemberUidList { get; }
}