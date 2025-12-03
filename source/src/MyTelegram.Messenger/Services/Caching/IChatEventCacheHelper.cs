namespace MyTelegram.Messenger.Services.Caching;
public interface IChatEventCacheHelper
{
    void Add(ChannelCreatedEvent data);
    void Add(long chatId, long migrateToChannelId);

    bool TryRemoveMigrateChannelId(long chatId, out long migrateToChannelId);
    bool TryGetMigrateChannelId(long chatId, out long migrateToChannelId);
    bool TryRemoveChannelCreatedEvent(long channelId,
        [NotNullWhen(true)] out ChannelCreatedEvent? channelCreatedEvent);
}