namespace MyTelegram.Domain.Commands.Dialog;

public class OutboxMessageHasReadCommand : RequestCommand2<DialogAggregate, DialogId, IExecutionResult>
{
    public OutboxMessageHasReadCommand(DialogId aggregateId,
        RequestInfo requestInfo,
        int maxMessageId,
        long ownerPeerId,
        Peer toPeer) : base(aggregateId, requestInfo)
    {
        MaxMessageId = maxMessageId;
        OwnerPeerId = ownerPeerId;
        ToPeer = toPeer;
    }

    public int MaxMessageId { get; }
    public long OwnerPeerId { get; }
    public Peer ToPeer { get; }
}