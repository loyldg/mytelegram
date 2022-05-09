namespace MyTelegram.Domain.Sagas.Events;

public class UpdatePinnedBoxPtsCompletedEvent : AggregateEvent<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId>
{
    public UpdatePinnedBoxPtsCompletedEvent(long peerId,
        int pts)
    {
        PeerId = peerId;
        Pts = pts;
    }

    public long PeerId { get; }
    public int Pts { get; }
}
