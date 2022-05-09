namespace MyTelegram.Domain.Commands.Dialog;

public class ClearParticipantHistoryCommand : Command<DialogAggregate, DialogId, IExecutionResult>, IHasCorrelationId
{
    public ClearParticipantHistoryCommand(DialogId aggregateId,
        Guid correlationId) : base(aggregateId)
    {
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
}
