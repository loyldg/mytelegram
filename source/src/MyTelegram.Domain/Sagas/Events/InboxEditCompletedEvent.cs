namespace MyTelegram.Domain.Sagas.Events;

public class InboxMessageEditCompletedEvent : AggregateEvent<EditMessageSaga, EditMessageSagaId>
{
    public InboxMessageEditCompletedEvent(
        long ownerPeerId,
        long senderPeerId,
        int messageId,
        string message,
        int editDate,
        int pts,
        Peer toPeer,
        byte[]? entities,
        byte[]? media
    )
    {
        OwnerPeerId = ownerPeerId;
        SenderPeerId = senderPeerId;
        MessageId = messageId;
        Message = message;
        Pts = pts;
        EditDate = editDate;
        ToPeer = toPeer;
        Entities = entities;
        Media = media;
    }

    public int EditDate { get; }
    public Peer ToPeer { get; }
    public byte[]? Entities { get; }
    public byte[]? Media { get; }
    public string Message { get; }
    public long OwnerPeerId { get; }
    public long SenderPeerId { get; }
    public int MessageId { get; }
    public int Pts { get; }
}
