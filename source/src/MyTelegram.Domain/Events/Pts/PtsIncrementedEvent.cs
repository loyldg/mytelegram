namespace MyTelegram.Domain.Events.Pts;

public class PtsIncrementedEvent : AggregateEvent<PtsAggregate, PtsId>, IHasCorrelationId
{
    public PtsIncrementedEvent(long peerId,
        int pts,
        PtsChangeReason reason,
        string messageBoxId,
        Guid correlationId)
    {
        PeerId = peerId;
        Pts = pts;
        Reason = reason;
        MessageBoxId = messageBoxId;
        CorrelationId = correlationId;
    }

    public string MessageBoxId { get; }

    public long PeerId { get; }

    /// <summary>
    ///     the new pts
    /// </summary>
    public int Pts { get; }

    public PtsChangeReason Reason { get; }
    public Guid CorrelationId { get; }
}
