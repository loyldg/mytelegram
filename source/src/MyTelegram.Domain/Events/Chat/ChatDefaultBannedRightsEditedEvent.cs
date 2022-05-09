namespace MyTelegram.Domain.Events.Chat;

public class ChatDefaultBannedRightsEditedEvent : RequestAggregateEvent<ChatAggregate, ChatId>
{
    public ChatDefaultBannedRightsEditedEvent(long reqMsgId,
        long chatId,
        ChatBannedRights defaultBannedRights,
        int version
    ) : base(reqMsgId)
    {
        ChatId = chatId;
        DefaultBannedRights = defaultBannedRights;
        Version = version;
    }

    public long ChatId { get; }
    public ChatBannedRights DefaultBannedRights { get; }
    public int Version { get; }
}
