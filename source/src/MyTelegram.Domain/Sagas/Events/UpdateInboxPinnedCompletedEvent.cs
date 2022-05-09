namespace MyTelegram.Domain.Sagas.Events;

public class UpdateInboxPinnedCompletedEvent : AggregateEvent<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId>
{
    public UpdateInboxPinnedCompletedEvent(long ownerPeerId,
        int messageId,
        Peer toPeer)
    {
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        ToPeer = toPeer;
    }

    public int MessageId { get; }
    public Peer ToPeer { get; }
    public long OwnerPeerId { get; } 
}
