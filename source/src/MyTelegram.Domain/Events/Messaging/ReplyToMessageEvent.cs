namespace MyTelegram.Domain.Events.Messaging;

public class ReplyToMessageEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
{
    public ReplyToMessageEvent(
        int senderMessageId,
        IReadOnlyList<InboxItem>? inboxItems,
        Guid correlationId)
    {
        SenderMessageId = senderMessageId;
        InboxItems = inboxItems;
        CorrelationId = correlationId;
    }

    public int SenderMessageId { get; }
    public IReadOnlyList<InboxItem>? InboxItems { get; }
    public Guid CorrelationId { get; }
}
