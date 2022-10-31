namespace MyTelegram.Domain.Sagas.Events;

public class DeleteParticipantHistoryPtsIncrementedEvent : AggregateEvent<DeleteParticipantHistorySaga, DeleteParticipantHistorySagaId>
{
    public long PeerId { get; }
    public int Pts { get; }

    public DeleteParticipantHistoryPtsIncrementedEvent(long peerId, int pts)
    {
        PeerId = peerId;
        Pts = pts;
    }
}