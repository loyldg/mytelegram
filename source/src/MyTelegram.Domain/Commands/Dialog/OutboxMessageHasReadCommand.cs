namespace MyTelegram.Domain.Commands.Dialog;

public class OutboxMessageHasReadCommand : Command<DialogAggregate, DialogId, IExecutionResult>, IHasCorrelationId
{
    public OutboxMessageHasReadCommand(DialogId aggregateId,
        long reqMsgId,
        int maxMessageId,
        long ownerPeerId,
        string sourceCommandId,
        Guid correlationId) : base(aggregateId)
    {
        ReqMsgId = reqMsgId;
        MaxMessageId = maxMessageId;
        OwnerPeerId = ownerPeerId;
        SourceCommandId = sourceCommandId;
        CorrelationId = correlationId;
    }

    public int MaxMessageId { get; }
    public long OwnerPeerId { get; }
    public long ReqMsgId { get; }
    public string SourceCommandId { get; }

    public Guid CorrelationId { get; }
}
