namespace MyTelegram.Domain.Sagas.Events;

public class OutboxMessageEditCompletedEvent : RequestAggregateEvent2<EditMessageSaga, EditMessageSagaId>
{
    public OutboxMessageEditCompletedEvent(RequestInfo requestInfo,
        long ownerPeerId,
        long senderPeerId,
        int messageId,
        bool post,
        int? views,
        string message,
        int pts,
        int date,
        Peer toPeer,
        byte[]? entities,
        byte[]? media
    ) : base(requestInfo)
    {
        OwnerPeerId = ownerPeerId;
        SenderPeerId = senderPeerId;
        MessageId = messageId;
        Post = post;
        Views = views;
        Message = message;
        Pts = pts;
        Date = date;
        ToPeer = toPeer;
        Entities = entities;
        Media = media;
    }

    public int Date { get; }
    public Peer ToPeer { get; }
    public byte[]? Entities { get; }
    public byte[]? Media { get; }
    public string Message { get; }
    public int MessageId { get; }
    public long OwnerPeerId { get; }
    public bool Post { get; }
    public int Pts { get; }

    public long SenderPeerId { get; }
    public int? Views { get; }
}
