namespace MyTelegram.Messenger.Services.Caching;

public class ChatEventCacheHelper : IChatEventCacheHelper
{
    private readonly ConcurrentDictionary<long, ChannelCreatedEvent> _channelCreatedEvents = new();
    private readonly ConcurrentDictionary<long, ChatCreatedEvent> _createdEvents = new();
    private readonly ConcurrentDictionary<long, int> _getCountDict = new();
    private readonly ConcurrentDictionary<long, StartInviteToChannelEvent> _inviteToChannelEvents = new();
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

    public void Add(StartInviteToChannelEvent data)
    {
        _inviteToChannelEvents.TryAdd(data.ChannelId, data);
    }

    public void Add(ChannelCreatedEvent data)
    {
        _channelCreatedEvents.TryAdd(data.ChannelId, data);
    }

    public void Add(ChatCreatedEvent data)
    {
        _getCountDict.TryAdd(data.ChatId, 0);
        _createdEvents.TryAdd(data.ChatId, data);
    }

    public bool TryRemoveStartInviteToChannelEvent(long channelId,
        [NotNullWhen(true)] out StartInviteToChannelEvent? eventData)
    {
        return _inviteToChannelEvents.TryRemove(channelId, out eventData);
    }

    public bool TryRemoveChannelCreatedEvent(long channelId,
        [NotNullWhen(true)] out ChannelCreatedEvent? channelCreatedEvent)
    {
        return _channelCreatedEvents.TryRemove(channelId, out channelCreatedEvent);
    }

    public bool TryRemoveChatCreatedEvent(long chatId,
        [NotNullWhen(true)] out ChatCreatedEvent? chatCreatedEvent)
    {
        return _createdEvents.TryRemove(chatId, out chatCreatedEvent);
    }

    public bool TryGetChatCreatedEvent(long chatId,
        [NotNullWhen(true)] out ChatCreatedEvent? chatCreatedEvent)
    {
        if (_getCountDict.TryGetValue(chatId, out var count))
        {
            _getCountDict.TryUpdate(chatId, count + 1, count);
        }

        var found = _createdEvents.TryGetValue(chatId, out chatCreatedEvent);

        if (found)
        {
            if (count == chatCreatedEvent!.MemberUidList.Count)
            {
                _getCountDict.TryRemove(chatId, out _);
                _createdEvents.TryRemove(chatId, out _);
            }
        }

        return found;
    }
}
