namespace MyTelegram.Domain.Commands.PushUpdates;

public class CreateEncryptedPushUpdatesCommand : Command<PushUpdatesAggregate, PushUpdatesId, IExecutionResult>
{
    public CreateEncryptedPushUpdatesCommand(PushUpdatesId aggregateId,
        long inboxOwnerPeerId,
        byte[] data,
        int qts,
        long inboxOwnerPermAuthKeyId) : base(aggregateId)
    {
        InboxOwnerPeerId = inboxOwnerPeerId;
        Data = data;
        Qts = qts;
        InboxOwnerPermAuthKeyId = inboxOwnerPermAuthKeyId;
    }

    public byte[] Data { get; }
    public long InboxOwnerPermAuthKeyId { get; }
    public long InboxOwnerPeerId { get; }
    public int Qts { get; }
}
