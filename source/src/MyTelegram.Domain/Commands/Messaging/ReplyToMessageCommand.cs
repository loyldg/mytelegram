namespace MyTelegram.Domain.Commands.Messaging;

public class ReplyToMessageCommand : RequestCommand<MessageAggregate, MessageId, IExecutionResult>
{
    public int MessageId { get; }
    public Guid CorrelationId { get; }

    public ReplyToMessageCommand(MessageId aggregateId,
        long reqMsgId,
        int messageId,
        Guid correlationId
    ) : base(aggregateId, reqMsgId)
    {
        MessageId = messageId;
        CorrelationId = correlationId;
    }
}