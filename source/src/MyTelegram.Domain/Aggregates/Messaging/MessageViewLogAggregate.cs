namespace MyTelegram.Domain.Aggregates.Messaging;

public class MessageViewLogAggregate : AggregateRoot<MessageViewLogAggregate, MessageViewLogId>,
    IApply<CheckMessageViewLogSuccessEvent>
{
    public MessageViewLogAggregate(MessageViewLogId id) : base(id)
    {
    }
    public void Apply(CheckMessageViewLogSuccessEvent aggregateEvent)
    {
    }

    public void CheckMessageViewLog(int messageId,
        Guid correlationId)
    {
        Emit(new CheckMessageViewLogSuccessEvent(messageId, !IsNew, correlationId));
    }
}