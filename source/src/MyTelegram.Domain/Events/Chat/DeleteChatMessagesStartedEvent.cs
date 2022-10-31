namespace MyTelegram.Domain.Events.Chat;

public class DeleteChatMessagesStartedEvent : RequestAggregateEvent2<ChatAggregate, ChatId>, IHasCorrelationId
{
    public List<int> MessageIds { get; }
    public bool Revoke { get; }
    public long ChatCreatorUserId { get; }
    public int ChatMemberCount { get; }
    public bool IsClearHistory { get; }
    public Guid CorrelationId { get; }

    public DeleteChatMessagesStartedEvent(RequestInfo requestInfo,
        List<int> messageIds,
        bool revoke,
        long chatCreatorUserId,
        int chatMemberCount,
        bool isClearHistory,
        Guid correlationId) : base(requestInfo)
    {
        MessageIds = messageIds;
        Revoke = revoke;
        ChatCreatorUserId = chatCreatorUserId;
        ChatMemberCount = chatMemberCount;
        IsClearHistory = isClearHistory;
        CorrelationId = correlationId;
    }
}