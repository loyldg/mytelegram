namespace MyTelegram.Domain.Aggregates.PushUpdates;

public class PushUpdatesAggregate : AggregateRoot<PushUpdatesAggregate, PushUpdatesId>
{
    private readonly PushUpdatesState _state = new();

    public PushUpdatesAggregate(PushUpdatesId id) : base(id)
    {
        Register(_state);
    }

    public void CreateEncryptedPushUpdate(long inboxOwnerPeerId,
        byte[] data,
        int qts,
        long inboxOwnerPermAuthKeyId)
    {
        var date = DateTime.UtcNow.ToTimestamp();
        Emit(new EncryptedPushUpdatesCreatedEvent(inboxOwnerPeerId,
            data,
            qts,
            inboxOwnerPermAuthKeyId,
            date
            ));
    }
}
