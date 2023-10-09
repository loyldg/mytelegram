namespace MyTelegram.Messenger.Services.Caching;
public interface IChatEventCacheHelper
{
    void Add(StartInviteToChannelEvent data);
    void Add(ChannelCreatedEvent data);
    void Add(ChatCreatedEvent data);
    void Add(long chatId, long migrateToChannelId);

    bool TryRemoveMigrateChannelId(long chatId, out long migrateToChannelId);
    bool TryGetMigrateChannelId(long chatId, out long migrateToChannelId);
    bool TryRemoveStartInviteToChannelEvent(long channelId,
        [NotNullWhen(true)] out StartInviteToChannelEvent? eventData);

    bool TryRemoveChannelCreatedEvent(long channelId,
        [NotNullWhen(true)] out ChannelCreatedEvent? channelCreatedEvent);

    bool TryRemoveChatCreatedEvent(long chatId,
        [NotNullWhen(true)] out ChatCreatedEvent? chatCreatedEvent);

    bool TryGetChatCreatedEvent(long chatId, [NotNullWhen(true)] out ChatCreatedEvent? chatCreatedEvent);
}