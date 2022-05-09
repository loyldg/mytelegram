namespace MyTelegram.Domain.Events.Messaging;

public class OutboxMessageCreatedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
{
    public long ReqMsgId { get; }
    public MessageItem OutboxMessageItem { get; }
    public bool ClearDraft { get; }
    public int GroupItemCount { get; }
    public Guid CorrelationId { get; }

    public OutboxMessageCreatedEvent(long reqMsgId, MessageItem outboxMessageItem, bool clearDraft, int groupItemCount, Guid correlationId)
    {
        ReqMsgId = reqMsgId;
        OutboxMessageItem = outboxMessageItem;
        ClearDraft = clearDraft;
        GroupItemCount = groupItemCount;
        CorrelationId = correlationId;
    }
}