namespace MyTelegram.Domain.Commands.Messaging;

public class DeleteSelfMessageCommand : Command<MessageAggregate, MessageId, IExecutionResult>, IHasCorrelationId
{
    public DeleteSelfMessageCommand(MessageId aggregateId,
        //RequestInfo requestInfo,
        int messageId,
        Guid correlationId) : base(aggregateId)
    {
        MessageId = messageId;
        CorrelationId = correlationId;
    }

    public int MessageId { get; }
    public Guid CorrelationId { get; }
}