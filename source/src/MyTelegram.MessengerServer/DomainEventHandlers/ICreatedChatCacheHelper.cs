namespace MyTelegram.MessengerServer.DomainEventHandlers;

public interface ICreatedChatCacheHelper
{
    void Add(StartInviteToChannelEvent data);
    void Add(ChannelCreatedEvent data);
    void Add(ChatCreatedEvent data);

    bool TryGetValue(long chatId,
        [NotNullWhen(true)] out ChatCreatedEvent? chatCreatedEvent);

    bool TryRemove(long channelId,
        [NotNullWhen(true)] out StartInviteToChannelEvent? eventData);

    bool TryRemove(long channelId,
        [NotNullWhen(true)] out ChannelCreatedEvent? channelCreatedEvent);
}