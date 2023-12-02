namespace MyTelegram.Domain.ValueObjects;

public class InboxItem : ValueObject
{
    public InboxItem(long inboxOwnerPeerId,
        int inboxMessageId)
    {
        InboxOwnerPeerId = inboxOwnerPeerId;
        InboxMessageId = inboxMessageId;
    }

    public int InboxMessageId { get; init; }

    public long InboxOwnerPeerId { get; init; }
}
