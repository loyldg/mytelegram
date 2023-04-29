namespace MyTelegram.Domain.Commands.Messaging;

public class DeleteOutboxMessageCommand : Command<MessageAggregate, MessageId, IExecutionResult>, IHasCorrelationId
{
    public DeleteOutboxMessageCommand(MessageId aggregateId,
        Guid correlationId) : base(aggregateId)
    {
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
}
