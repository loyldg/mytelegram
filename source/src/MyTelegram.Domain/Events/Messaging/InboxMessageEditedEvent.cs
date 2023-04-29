namespace MyTelegram.Domain.Events.Messaging;

public class InboxMessageEditedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
{
    public InboxMessageEditedEvent(
        long inboxOwnerPeerId,
        int messageId,
        string newMessage,
        byte[]? entities,
        int editDate,
        Peer toPeer,
        byte[]? media,
        Guid correlationId)
    {
        InboxOwnerPeerId = inboxOwnerPeerId;
        MessageId = messageId;
        NewMessage = newMessage;
        Entities = entities;
        EditDate = editDate;
        ToPeer = toPeer;
        Media = media;
        CorrelationId = correlationId;
    }

    public Peer ToPeer { get; }
    public byte[]? Entities { get; }
    public int EditDate { get; }
    public long InboxOwnerPeerId { get; }
    public byte[]? Media { get; }

    public int MessageId { get; }
    public string NewMessage { get; }
    public Guid CorrelationId { get; }
}
