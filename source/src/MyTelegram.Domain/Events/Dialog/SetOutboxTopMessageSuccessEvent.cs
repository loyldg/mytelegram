namespace MyTelegram.Domain.Events.Dialog;

public class SetOutboxTopMessageSuccessEvent : AggregateEvent<DialogAggregate, DialogId>
{
    public SetOutboxTopMessageSuccessEvent(
        //RequestInfo requestInfo,
        //MessageBoxId messageBoxId,
        int messageId,
        long ownerPeerId,
        //int pts,
        Peer toPeer,
        bool clearDraft
    )
    {
        //MessageBoxId = messageBoxId;
        MessageId = messageId;

        ClearDraft = clearDraft;
        OwnerPeerId = ownerPeerId;
        ToPeer = toPeer;
        //Pts = pts;
    }

    public bool ClearDraft { get; }

    public int MessageId { get; }
    public long OwnerPeerId { get; }
    public Peer ToPeer { get; }

    //public MessageBoxId MessageBoxId { get; }

}