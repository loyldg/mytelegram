namespace MyTelegram.MessengerServer.DomainEventHandlers;

public class CreatedChatCacheHelper : ICreatedChatCacheHelper
{
    private readonly ConcurrentDictionary<long, ChatCreatedEvent> _createdEvents = new();
    private readonly ConcurrentDictionary<long, int> _getCountDict = new();

    private readonly ConcurrentDictionary<long, ChannelCreatedEvent> _channelCreatedEvents = new();
    private readonly ConcurrentDictionary<long, StartInviteToChannelEvent> _inviteToChannelEvents = new();

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

    public bool TryRemove(long channelId, [NotNullWhen(true)] out StartInviteToChannelEvent? eventData)
    {
        return _inviteToChannelEvents.TryRemove(channelId, out eventData);
    }

    public bool TryRemove(long channelId,
        [NotNullWhen(true)] out ChannelCreatedEvent? channelCreatedEvent)
    {
        return _channelCreatedEvents.TryRemove(channelId, out channelCreatedEvent);
    }

    public bool TryGetValue(long chatId,
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