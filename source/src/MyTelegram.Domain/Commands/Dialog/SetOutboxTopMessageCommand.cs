namespace MyTelegram.Domain.Commands.Dialog;

public class SetOutboxTopMessageCommand : /*Request*/Command<DialogAggregate, DialogId, IExecutionResult>
{
    public SetOutboxTopMessageCommand(DialogId aggregateId,
        //long reqMsgId,
        int messageId,
        long ownerPeerId,
        //int pts,
        Peer toPeer,
        bool clearDraft,
        Guid correlationId
    ) : base(aggregateId /*, reqMsgId*/)
    {
        //MessageBoxId = messageBoxId;
        MessageId = messageId;
        OwnerPeerId = ownerPeerId;
        //Pts = pts;
        CorrelationId = correlationId;
        ToPeer = toPeer;
        ClearDraft = clearDraft;
    }

    public bool ClearDraft { get; }

    public Guid CorrelationId { get; }

    //public MessageBoxId MessageBoxId { get; }
    public int MessageId { get; }

    //public int Pts { get; }
    public long OwnerPeerId { get; }
    public Peer ToPeer { get; }
}
