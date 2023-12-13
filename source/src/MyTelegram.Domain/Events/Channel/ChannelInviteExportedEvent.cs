namespace MyTelegram.Domain.Events.Channel;

public class ChannelInviteExportedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public ChannelInviteExportedEvent(
        RequestInfo requestInfo,
        long channelId,
        long adminId,
        long inviteId,
        string? title,
        bool requestNeeded,
        int? expireDate,
        int? usageLimit,
        bool revoke,
        string link,
        bool permanent,
        int date,
        int startDate) : base(requestInfo)
    {
        ChannelId = channelId;
        AdminId = adminId;
        InviteId = inviteId;
        Title = title;
        RequestNeeded = requestNeeded;
        ExpireDate = expireDate;
        UsageLimit = usageLimit;
        Revoke = revoke;
        Link = link;
        Permanent = permanent;
        Date = date;
        StartDate = startDate;
    }

    public long AdminId { get; }
    public long InviteId { get; }
    public string? Title { get; }
    public bool RequestNeeded { get; }
    public long ChannelId { get; }
    public int Date { get; }
    public int? ExpireDate { get; }
    public string Link { get; }

    public bool Permanent { get; }

    //public bool LegacyRevokePermanent { get; }
    public bool Revoke { get; }
    public int StartDate { get; }
    public int? UsageLimit { get; }
}