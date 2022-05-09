namespace MyTelegram.Domain.Events.Messaging;

public class CheckMessageViewLogSuccessEvent : AggregateEvent<MessageViewLogAggregate, MessageViewLogId>,
    IHasCorrelationId
{
    public CheckMessageViewLogSuccessEvent(int messageId,
        bool alreadyIncremented,
        Guid correlationId)
    {
        AlreadyIncremented = alreadyIncremented;
        CorrelationId = correlationId;
        MessageId = messageId;
    }

    public bool AlreadyIncremented { get; }

    public int MessageId { get; }
    public Guid CorrelationId { get; }
}
