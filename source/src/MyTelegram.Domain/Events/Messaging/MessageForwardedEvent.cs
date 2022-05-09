namespace MyTelegram.Domain.Events.Messaging;

public class MessageForwardedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>, IHasCorrelationId
{
    public long RandomId { get; }
    public MessageItem OriginalMessageItem { get; }
    public Guid CorrelationId { get; }

    public MessageForwardedEvent(RequestInfo request, long randomId, MessageItem originalMessageItem, Guid correlationId) : base(request)
    {
        RandomId = randomId;
        OriginalMessageItem = originalMessageItem;
        CorrelationId = correlationId;
    }
}