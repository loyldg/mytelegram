namespace MyTelegram.Domain.Events.Chat;

public class ChatDeletedEvent : RequestAggregateEvent2<ChatAggregate, ChatId>
{
    public ChatDeletedEvent(RequestInfo requestInfo, long chatId, string title) : base(requestInfo)
    {
        ChatId = chatId;
        Title = title;
    }

    public long ChatId { get; }
    public string Title { get; }
}
