namespace MyTelegram.Domain.Events.Channel;

public class ChannelInviteEditedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public ChannelInviteEditedEvent(
        RequestInfo requestInfo,
        long channelId,
        long inviteId,
        long adminId,
        //long inviteId,
        string? title,
        bool requestNeeded,
        int startDate,
        int? expireDate,
        int? usageLimit,
        bool revoke,
        string link,
        bool permanent,
        int requested,
        int usage

    ) : base(requestInfo)
    {
        ChannelId = channelId;
        InviteId = inviteId;
        AdminId = adminId;
        //InviteId = inviteId;
        Title = title;
        RequestNeeded = requestNeeded;
        StartDate = startDate;
        ExpireDate = expireDate;
        UsageLimit = usageLimit;
        Revoke = revoke;
        Link = link;
        Permanent = permanent;
        Requested = requested;
        Usage = usage;
    }

    public long AdminId { get; }
    //public long InviteId { get; }
    public string? Title { get; }
    public bool RequestNeeded { get; }
    public int StartDate { get; }
    public long ChannelId { get; }
    public long InviteId { get; }
    public int? ExpireDate { get; }
    public string Link { get; }

    public bool Permanent { get; }
    public int Requested { get; }
    public int Usage { get; }

    public bool Revoke { get; }
    public int? UsageLimit { get; }
}