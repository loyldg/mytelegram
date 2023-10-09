namespace MyTelegram.Domain.Events.Channel;

public class IncrementParticipantCountEvent : AggregateEvent<ChannelAggregate, ChannelId> //, IHasCorrelationId
{
    public long ChannelId { get; }
    public int NewParticipantCount { get; }

    public IncrementParticipantCountEvent(long channelId, int newParticipantCount)
    {
        ChannelId = channelId;
        NewParticipantCount = newParticipantCount;
    }

    //public IncrementParticipantCountEvent(Guid correlationId)
    //{
    //    
    //}

    //
}

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

public class ChannelInviteDeletedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public long ChannelId { get; }
    public long InviteId { get; }

    public ChannelInviteDeletedEvent(RequestInfo requestInfo, long channelId, long inviteId) : base(requestInfo)
    {
        ChannelId = channelId;
        InviteId = inviteId;
    }
}