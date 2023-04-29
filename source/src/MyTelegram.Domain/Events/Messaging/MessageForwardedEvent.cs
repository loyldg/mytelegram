namespace MyTelegram.Domain.Events.Messaging;

public class MessageForwardedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>, IHasCorrelationId
{
    public MessageForwardedEvent(RequestInfo requestInfo,
        long randomId,
        MessageItem originalMessageItem,
        Guid correlationId) : base(requestInfo)
    {
        RandomId = randomId;
        OriginalMessageItem = originalMessageItem;
        CorrelationId = correlationId;
    }

    public long RandomId { get; }
    public MessageItem OriginalMessageItem { get; }
    public Guid CorrelationId { get; }
}
