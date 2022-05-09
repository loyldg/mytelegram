namespace MyTelegram.Domain.Events.Channel;

public class ExportChatInviteEvent : RequestAggregateEvent<ChannelAggregate, ChannelId>
{
    public ExportChatInviteEvent(long reqMsgId,
        long channelId,
        long adminId,
        int? expireDate,
        int? usageLimit,
        bool revoke,
        string link,
        bool permanent,
        int date,
        int startDate) : base(reqMsgId)
    {
        ChannelId = channelId;
        AdminId = adminId;
        ExpireDate = expireDate;
        UsageLimit = usageLimit;
        Revoke = revoke;
        Link = link;
        Permanent = permanent;
        Date = date;
        StartDate = startDate;
    }

    public long AdminId { get; }
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
