namespace MyTelegram.Domain.Events.Chat;

public class ChatDefaultBannedRightsEditedEvent : RequestAggregateEvent2<ChatAggregate, ChatId>
{
    public ChatDefaultBannedRightsEditedEvent(RequestInfo requestInfo,
        long chatId,
        ChatBannedRights defaultBannedRights,
        int version
    ) : base(requestInfo)
    {
        ChatId = chatId;
        DefaultBannedRights = defaultBannedRights;
        Version = version;
    }

    public long ChatId { get; }
    public ChatBannedRights DefaultBannedRights { get; }
    public int Version { get; }
}
