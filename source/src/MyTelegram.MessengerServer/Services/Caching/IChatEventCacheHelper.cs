namespace MyTelegram.MessengerServer.Services.Caching;

public interface IChatEventCacheHelper
{
    void Add(StartInviteToChannelEvent data);
    void Add(ChannelCreatedEvent data);
    void Add(ChatCreatedEvent data);

    bool TryGetChatCreatedEvent(long chatId,
        [NotNullWhen(true)] out ChatCreatedEvent? chatCreatedEvent);

    bool TryRemoveChannelCreatedEvent(long channelId,
        [NotNullWhen(true)] out ChannelCreatedEvent? channelCreatedEvent);

    bool TryRemoveChatCreatedEvent(long chatId,
        [NotNullWhen(true)] out ChatCreatedEvent? chatCreatedEvent);

    bool TryRemoveStartInviteToChannelEvent(long channelId,
        [NotNullWhen(true)] out StartInviteToChannelEvent? eventData);
}
