namespace MyTelegram.Domain.Events.Chat;

public class ClearGroupChatHistoryStartedEvent : RequestAggregateEvent2<ChatAggregate, ChatId>
{
    public ClearGroupChatHistoryStartedEvent(
        RequestInfo requestInfo,
        long chatId,
        IReadOnlyList<ChatMember> memberUidList) : base(requestInfo)
    {
        ChatId = chatId;
        MemberUidList = memberUidList;

    }

    public long ChatId { get; }
    public IReadOnlyList<ChatMember> MemberUidList { get; }

}
