namespace MyTelegram.Domain.Commands.Dialog;

public class SetOutboxTopMessageCommand : /*Request*/Command<DialogAggregate, DialogId, IExecutionResult>
{
    public SetOutboxTopMessageCommand(DialogId aggregateId,
        //long reqMsgId,
        //RequestInfo requestInfo,
        int messageId,
        long ownerPeerId,
        //int pts,
        Peer toPeer,
        bool clearDraft
    ) : base(aggregateId)
    {
        //MessageBoxId = messageBoxId;
        MessageId = messageId;
        OwnerPeerId = ownerPeerId;
        //Pts = pts; 
        ToPeer = toPeer;
        ClearDraft = clearDraft;
    }

    public bool ClearDraft { get; }

    //public MessageBoxId MessageBoxId { get; }
    public int MessageId { get; }

    //public int Pts { get; }
    public long OwnerPeerId { get; }
    public Peer ToPeer { get; }

}