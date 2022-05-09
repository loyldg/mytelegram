namespace MyTelegram.Domain.Events.Messaging;

public class InboxMessageCreatedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
{
    public MessageItem InboxMessageItem { get; }
    public int SenderMessageId { get; }

    public InboxMessageCreatedEvent(MessageItem inboxMessageItem, int senderMessageId,
        Guid correlationId)
    {
        InboxMessageItem = inboxMessageItem;
        SenderMessageId = senderMessageId;
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
}