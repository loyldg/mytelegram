namespace MyTelegram.Domain.Commands.Messaging;

public class DeleteInboxMessageCommand : Command<MessageAggregate, MessageId, IExecutionResult>, IHasCorrelationId
{
    public DeleteInboxMessageCommand(MessageId aggregateId,
        Guid correlationId) : base(aggregateId)
    {
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
}
