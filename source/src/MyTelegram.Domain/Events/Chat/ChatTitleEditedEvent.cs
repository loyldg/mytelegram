namespace MyTelegram.Domain.Events.Chat;

public class ChatTitleEditedEvent : RequestAggregateEvent2<ChatAggregate, ChatId>
{
    public ChatTitleEditedEvent(RequestInfo requestInfo,
        long chatId,
        string title,
        string messageActionData,
        long randomId) : base(requestInfo)
    {
        ChatId = chatId;
        Title = title;
        MessageActionData = messageActionData;
        RandomId = randomId;
    }

    public long ChatId { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }
    public string Title { get; }
}

