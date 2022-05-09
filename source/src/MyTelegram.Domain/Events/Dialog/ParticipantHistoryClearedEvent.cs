namespace MyTelegram.Domain.Events.Dialog;

public class ParticipantHistoryClearedEvent : AggregateEvent<DialogAggregate, DialogId>, IHasCorrelationId
{
    public ParticipantHistoryClearedEvent(long ownerPeerId,
        int historyMinId,
        Guid correlationId)
    {
        OwnerPeerId = ownerPeerId;
        HistoryMinId = historyMinId;
        CorrelationId = correlationId;
    }

    public int HistoryMinId { get; }

    public long OwnerPeerId { get; }
    public Guid CorrelationId { get; }
}
