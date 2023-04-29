namespace MyTelegram.Domain.Events.Dialog;

public class SetOutboxTopMessageSuccessEvent : AggregateEvent<DialogAggregate, DialogId>, IHasCorrelationId
{
    public SetOutboxTopMessageSuccessEvent(
        //MessageBoxId messageBoxId,
        int messageId,
        long ownerPeerId,
        //int pts,
        Peer toPeer,
        bool clearDraft,
        Guid correlationId
    )
    {
        //MessageBoxId = messageBoxId;
        MessageId = messageId;
        CorrelationId = correlationId;
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
    public Guid CorrelationId { get; }
}
