namespace MyTelegram.Domain.Commands.Dialog;

public class ReceiveInboxMessageCommand : /*RequestInfo*/Command<DialogAggregate, DialogId, IExecutionResult>
{
    public ReceiveInboxMessageCommand(DialogId aggregateId,
        //long reqMsgId,
        //MessageBoxId messageBoxId,
        int messageId,
        long ownerPeerId,
        //int pts,
        Peer toPeer,
        Guid correlationId) : base(aggregateId /*, reqMsgId*/)
    {
        //MessageBoxId = messageBoxId;
        MessageId = messageId;
        OwnerPeerId = ownerPeerId;
        //Pts = pts;
        ToPeer = toPeer;
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }

    //public MessageBoxId MessageBoxId { get; }
    public int MessageId { get; }
    public long OwnerPeerId { get; }

    //public int Pts { get; }
    public Peer ToPeer { get; }
}
