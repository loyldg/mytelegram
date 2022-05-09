namespace MyTelegram.Domain.Events.Chat;

public class ClearGroupChatHistoryStartedEvent : AggregateEvent<ChatAggregate, ChatId>, IHasCorrelationId
{
    public ClearGroupChatHistoryStartedEvent(long chatId,
        IReadOnlyList<ChatMember> memberUidList,
        Guid correlationId)
    {
        ChatId = chatId;
        MemberUidList = memberUidList;
        CorrelationId = correlationId;
    }

    public long ChatId { get; }
    public IReadOnlyList<ChatMember> MemberUidList { get; }
    public Guid CorrelationId { get; }
}
