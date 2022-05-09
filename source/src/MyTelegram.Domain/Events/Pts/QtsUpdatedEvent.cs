namespace MyTelegram.Domain.Events.Pts;

public class QtsUpdatedEvent : AggregateEvent<PtsAggregate, PtsId>
{
    public QtsUpdatedEvent(long peerId,
        int newQts)
    {
        PeerId = peerId;
        NewQts = newQts;
    }

    public int NewQts { get; }

    public long PeerId { get; }
}
