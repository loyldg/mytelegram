namespace MyTelegram.Domain.Sagas.Events;

public class DeleteMessagePtsIncrementedEvent : AggregateEvent<DeleteMessageSaga, DeleteMessageSagaId>
{
    public DeleteMessagePtsIncrementedEvent(long peerId,
        int pts)
    {
        PeerId = peerId;
        Pts = pts;
    }

    public long PeerId { get; }
    public int Pts { get; }
}
