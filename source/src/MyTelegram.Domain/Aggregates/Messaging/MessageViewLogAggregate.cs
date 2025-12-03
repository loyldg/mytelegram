namespace MyTelegram.Domain.Aggregates.Messaging;

public class MessageViewLogAggregate : AggregateRoot<MessageViewLogAggregate, MessageViewLogId>
{
    private readonly MessageViewLogState _state = new();

    public MessageViewLogAggregate(MessageViewLogId id) : base(id)
    {
        Register(_state);
    }

    public void CheckMessageViewLog(
        RequestInfo requestInfo,
        int messageId)
    {
        var alreadyIncremented = !IsNew;
        Emit(new CheckMessageViewLogSuccessEvent(requestInfo, messageId, alreadyIncremented));
    }
}