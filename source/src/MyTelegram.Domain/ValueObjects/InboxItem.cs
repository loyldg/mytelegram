namespace MyTelegram.Domain.ValueObjects;

public class InboxItem : ValueObject
{
    public InboxItem(long inboxOwnerPeerId,
        int inboxMessageId)
    {
        InboxOwnerPeerId = inboxOwnerPeerId;
        InboxMessageId = inboxMessageId;
    }

    public int InboxMessageId { get; }

    public long InboxOwnerPeerId { get; }
}
