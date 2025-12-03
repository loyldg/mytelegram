namespace MyTelegram.Messenger.Services.Caching;

public class ChatEventCacheHelper : IChatEventCacheHelper, ISingletonDependency
{
    private readonly ConcurrentDictionary<long, ChannelCreatedEvent> _channelCreatedEvents = new();
    //private readonly ConcurrentDictionary<long, StartInviteToChannelEvent> _inviteToChannelEvents = new();
    private readonly ConcurrentDictionary<long, long> _chatIdToMigrateToChannelIds = new();

    public void Add(long chatId, long migrateToChannelId)
    {
        _chatIdToMigrateToChannelIds.TryAdd(chatId, migrateToChannelId);
    }

    public bool TryRemoveMigrateChannelId(long chatId, out long migrateToChannelId)
    {
        return _chatIdToMigrateToChannelIds.TryRemove(chatId, out migrateToChannelId);
    }

    public bool TryGetMigrateChannelId(long chatId, out long migrateToChannelId)
    {
        return _chatIdToMigrateToChannelIds.TryGetValue(chatId, out migrateToChannelId);
    }

    //public void Add(StartInviteToChannelEvent data)
    //{
    //    _inviteToChannelEvents.TryAdd(data.ChannelId, data);
    //}

    public void Add(ChannelCreatedEvent data)
    {
        _channelCreatedEvents.TryAdd(data.ChannelId, data);
    }

    //public bool TryRemoveStartInviteToChannelEvent(long channelId,
    //    [NotNullWhen(true)] out StartInviteToChannelEvent? eventData)
    //{
    //    return _inviteToChannelEvents.TryRemove(channelId, out eventData);
    //}

    public bool TryRemoveChannelCreatedEvent(long channelId,
        [NotNullWhen(true)] out ChannelCreatedEvent? channelCreatedEvent)
    {
        return _channelCreatedEvents.TryRemove(channelId, out channelCreatedEvent);
    }
}
