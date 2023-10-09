namespace MyTelegram.Domain.Events.Channel;

public class ChannelCreatedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public ChannelCreatedEvent(RequestInfo requestInfo,
        long channelId,
        long creatorId,
        string title,
        bool broadcast,
        bool megaGroup,
        string? about,
        string? address,
        long accessHash,
        int date,
        long randomId,
        string messageActionData,
        int? ttlPeriod,
        bool migratedFromChat,
        long? migratedFromChatId,
        int? migratedMaxId,
        long? photoId) : base(requestInfo)
    {
        ChannelId = channelId;
        CreatorId = creatorId;
        Title = title;
        Broadcast = broadcast;
        MegaGroup = megaGroup;
        About = about;
        Address = address;
        AccessHash = accessHash;
        Date = date;
        RandomId = randomId;
        MessageActionData = messageActionData;
        TtlPeriod = ttlPeriod;
        MigratedFromChat = migratedFromChat;
        MigratedFromChatId = migratedFromChatId;
        MigratedMaxId = migratedMaxId;
        PhotoId = photoId;
    }

    public string? About { get; }
    public long AccessHash { get; }
    public string? Address { get; }

    public bool Broadcast { get; }
    public long ChannelId { get; }
    public long CreatorId { get; }
    public int Date { get; }
    public bool MegaGroup { get; }
    public string MessageActionData { get; }
    public int? TtlPeriod { get; }
    public bool MigratedFromChat { get; }
    public long? MigratedFromChatId { get; }
    public int? MigratedMaxId { get; }
    public long? PhotoId { get; }
    public long RandomId { get; }
    public string Title { get; }
}