namespace MyTelegram.Domain.Aggregates.Messaging;

public class MessageViewLogState:AggregateState<MessageViewLogAggregate, MessageViewLogId, MessageViewLogState>,
    IApply<CheckMessageViewLogSuccessEvent>
{
    public void Apply(CheckMessageViewLogSuccessEvent aggregateEvent)
    {
    }
}
