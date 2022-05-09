namespace MyTelegram.Domain.Aggregates.PushUpdates;

public class PushUpdatesAggregate : AggregateRoot<PushUpdatesAggregate, PushUpdatesId>
{
    private readonly PushUpdatesState _state = new();

    public PushUpdatesAggregate(PushUpdatesId id) : base(id)
    {
        Register(_state);
    }

    public void Create(
        Peer toPeer,
        long excludeAuthKeyId,
        long excludeUid,
        long onlySendToThisAuthKeyId,
        byte[] data,
        int pts,
        PtsType ptsType,
        long seqNo
    )
    {
        Emit(new PushUpdatesCreatedEvent(toPeer,
            excludeAuthKeyId,
            excludeUid,
            onlySendToThisAuthKeyId,
            data,
            pts,
            ptsType,
            seqNo,
            DateTime.UtcNow.ToTimestamp()));
    }

    public void CreateEncryptedPushUpdate(long inboxOwnerPeerId,
        byte[] data,
        int qts,
        long inboxOwnerPermAuthKeyId)
    {
        Emit(new EncryptedPushUpdatesCreatedEvent(inboxOwnerPeerId,
            data,
            qts,
            inboxOwnerPermAuthKeyId,
            DateTime.UtcNow.ToTimestamp()));
    }
}
