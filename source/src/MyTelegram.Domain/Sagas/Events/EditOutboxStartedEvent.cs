namespace MyTelegram.Domain.Sagas.Events;

public class EditOutboxMessageStartedEvent : RequestAggregateEvent2<EditMessageSaga, EditMessageSagaId>
{
    public EditOutboxMessageStartedEvent(
        RequestInfo request,
        MessageItem oldMessageItem,
        int messageId,
        string newMessage,
        int editDate,
        int inboxCount,
        byte[]? entities,
        byte[]? media
    ):base(request)
    { 
        OldMessageItem = oldMessageItem;
        MessageId = messageId;
        NewMessage = newMessage;
        Entities = entities;
        EditDate = editDate;
        InboxCount = inboxCount;
        Media = media;
    }

    public int EditDate { get; }
    public byte[]? Entities { get; }
    public int InboxCount { get; }
    public byte[]? Media { get; }
    public string NewMessage { get; }
    public int MessageId { get; } 
    public MessageItem OldMessageItem { get; }
}
