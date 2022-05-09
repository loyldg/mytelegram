namespace MyTelegram.Domain.Events.PushUpdates;

public class EncryptedPushUpdatesCreatedEvent : AggregateEvent<PushUpdatesAggregate, PushUpdatesId>
{
    public EncryptedPushUpdatesCreatedEvent(long inboxOwnerPeerId,
        byte[] data,
        int qts,
        long inboxOwnerPermAuthKeyId,
        int date)
    {
        InboxOwnerPeerId = inboxOwnerPeerId;
        Data = data;
        Qts = qts;
        InboxOwnerPermAuthKeyId = inboxOwnerPermAuthKeyId;
        Date = date;
    }

    public byte[] Data { get; }
    public int Date { get; }
    public long InboxOwnerPermAuthKeyId { get; }
    public long InboxOwnerPeerId { get; }
    public int Qts { get; }
}
