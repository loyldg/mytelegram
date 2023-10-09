namespace MyTelegram.Domain.Commands.Channel;

public class EditChannelInviteCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>
{
    public long AdminId { get; }
    public long InviteId { get; }
    public string? Title { get; }
    public bool RequestNeeded { get; }
    public bool Revoked { get; }
    public int StartDate { get; }
    public int? ExpireDate { get; }
    public int? UsageLimit { get; }
    public string Link { get; }
    public bool Permanent { get; }
    public int Requested { get; }
    public int Usage { get; }

    public EditChannelInviteCommand(ChannelId aggregateId, RequestInfo requestInfo,
        long adminId,
        long inviteId,
        string? title,
        bool requestNeeded,
        bool revoked,
        int startDate,
        int? expireDate,
        int? usageLimit,
        string link,
        bool permanent,
        int requested,
        int usage) : base(aggregateId, requestInfo)
    {
        AdminId = adminId;
        InviteId = inviteId;
        Title = title;
        RequestNeeded = requestNeeded;
        Revoked = revoked;
        StartDate = startDate;
        ExpireDate = expireDate;
        UsageLimit = usageLimit;
        Link = link;
        Permanent = permanent;
        Requested = requested;
        Usage = usage;
    }
}

public class ExportChatInviteCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>
{
    public ExportChatInviteCommand(ChannelId aggregateId,
        RequestInfo requestInfo,
        long adminId,
        long inviteId,
        string? title,
        bool requestNeeded,
        int? expireDate,
        int? usageLimit,
        bool legacyRevokePermanent,
        string randomLink,
        int date) : base(aggregateId, requestInfo)
    {
        AdminId = adminId;
        InviteId = inviteId;
        Title = title;
        RequestNeeded = requestNeeded;
        ExpireDate = expireDate;
        UsageLimit = usageLimit;
        LegacyRevokePermanent = legacyRevokePermanent;
        RandomLink = randomLink;
        Date = date;
    }

    public long AdminId { get; }
    public long InviteId { get; }
    public string? Title { get; }
    public bool RequestNeeded { get; }
    public int Date { get; }
    public int? ExpireDate { get; }
    public bool LegacyRevokePermanent { get; }
    public string RandomLink { get; }
    public int? UsageLimit { get; }
}
