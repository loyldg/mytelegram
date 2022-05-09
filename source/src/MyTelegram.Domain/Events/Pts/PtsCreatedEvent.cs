namespace MyTelegram.Domain.Events.Pts;

public class PtsCreatedEvent : AggregateEvent<PtsAggregate, PtsId>, IHasCorrelationId
{
    public PtsCreatedEvent(long peerId,
        int pts,
        int qts,
        int unreadCount,
        int date,
        Guid correlationId)
    {
        PeerId = peerId;
        Pts = pts;
        Qts = qts;
        UnreadCount = unreadCount;
        Date = date;
        CorrelationId = correlationId;
    }

    public int Date { get; }

    public long PeerId { get; }
    public int Pts { get; }
    public int Qts { get; }
    public int UnreadCount { get; }
    public Guid CorrelationId { get; }
}
