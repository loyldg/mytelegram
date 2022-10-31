namespace MyTelegram.Domain.Sagas.Events;

public class DeleteMessagePtsIncrementedEvent2 : AggregateEvent<DeleteMessageSaga2, DeleteMessageSaga2Id>
{
    public DeleteMessagePtsIncrementedEvent2(long peerId,
        int pts)
    {
        PeerId = peerId;
        Pts = pts;
    }

    public long PeerId { get; }
    public int Pts { get; }
}