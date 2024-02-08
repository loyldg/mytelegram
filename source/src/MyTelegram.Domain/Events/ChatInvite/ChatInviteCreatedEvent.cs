namespace MyTelegram.Domain.Events.ChatInvite;

public class ChatInviteCreatedEvent : RequestAggregateEvent2<ChatInviteAggregate, ChatInviteId>
{
    public ChatInviteCreatedEvent(RequestInfo requestInfo, long channelId, long inviteId, string hash, long adminId, string? title,
        bool requestNeeded, int? startDate, int? expireDate, int? usageLimit, bool permanent, int date) : base(requestInfo)
    {
        ChannelId = channelId;
        InviteId = inviteId;
        Hash = hash;
        AdminId = adminId;
        Title = title;
        RequestNeeded = requestNeeded;
        StartDate = startDate;
        ExpireDate = expireDate;
        UsageLimit = usageLimit;
        Permanent = permanent;
        Date = date;
    }

    public long ChannelId { get; }
    public long InviteId { get; }
    public string Hash { get; }
    public long AdminId { get; }
    public string? Title { get; }
    public bool RequestNeeded { get; }
    public int? StartDate { get; }
    public int? ExpireDate { get; }
    public int? UsageLimit { get; }
    public bool Permanent { get; }
    public int Date { get; }
}