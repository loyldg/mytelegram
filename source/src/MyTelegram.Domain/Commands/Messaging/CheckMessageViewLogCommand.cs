namespace MyTelegram.Domain.Commands.Messaging;

public class CheckMessageViewLogCommand : Command<MessageViewLogAggregate, MessageViewLogId, IExecutionResult>,
    IHasCorrelationId
{
    public CheckMessageViewLogCommand(MessageViewLogId aggregateId,
        int messageId,
        Guid correlationId) : base(aggregateId)
    {
        MessageId = messageId;
        CorrelationId = correlationId;
    }

    public int MessageId { get; }

    public Guid CorrelationId { get; }
}
