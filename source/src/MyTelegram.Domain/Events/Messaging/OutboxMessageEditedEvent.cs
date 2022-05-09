namespace MyTelegram.Domain.Events.Messaging;

public class OutboxMessageEditedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>, IHasCorrelationId
{
    public OutboxMessageEditedEvent(RequestInfo request,
        IReadOnlyCollection<InboxItem>? inboxItems,
        MessageItem oldMessageItem,
        int messageId,
        string newMessage,
        int editDate,
        byte[]? entities,
        byte[]? media,
        Guid correlationId) : base(request)
    {
        InboxItems = inboxItems;
        OldMessageItem = oldMessageItem;
        MessageId = messageId;
        NewMessage = newMessage;
        Entities = entities;
        Media = media;
        EditDate = editDate;
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
    public IReadOnlyCollection<InboxItem>? InboxItems { get; }
    public MessageItem OldMessageItem { get; }
    public int MessageId { get; }
    public string NewMessage { get; }
    public byte[]? Entities { get; }
    public byte[]? Media { get; }
    public int EditDate { get; }
}