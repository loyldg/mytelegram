namespace MyTelegram.Domain.Events.Chat;

public class ChatAboutEditedEvent : RequestAggregateEvent2<ChatAggregate, ChatId>
{
    public ChatAboutEditedEvent(RequestInfo requestInfo,
        string? about) : base(requestInfo)
    {
        About = about;
    }

    public string? About { get; }
}