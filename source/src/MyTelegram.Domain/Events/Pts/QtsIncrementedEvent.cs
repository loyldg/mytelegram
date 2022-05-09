namespace MyTelegram.Domain.Events.Pts;

public class QtsIncrementedEvent : AggregateEvent<PtsAggregate, PtsId>, IHasCorrelationId
{
    public QtsIncrementedEvent(long peerId,
        int qts,
        string encryptedMessageBoxId,
        Guid correlationId)
    {
        PeerId = peerId;
        Qts = qts;
        EncryptedMessageBoxId = encryptedMessageBoxId;
        CorrelationId = correlationId;
    }

    public string EncryptedMessageBoxId { get; }

    public long PeerId { get; }
    public int Qts { get; }
    public Guid CorrelationId { get; }
}
