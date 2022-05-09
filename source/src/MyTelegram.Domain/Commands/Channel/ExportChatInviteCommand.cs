namespace MyTelegram.Domain.Commands.Channel;

public class ExportChatInviteCommand : RequestCommand<ChannelAggregate, ChannelId, IExecutionResult>
{
    public ExportChatInviteCommand(ChannelId aggregateId,
        long reqMsgId,
        long adminId,
        int? expireDate,
        int? usageLimit,
        bool legacyRevokePermanent,
        string randomLink,
        int date) : base(aggregateId, reqMsgId)
    {
        AdminId = adminId;
        ExpireDate = expireDate;
        UsageLimit = usageLimit;
        LegacyRevokePermanent = legacyRevokePermanent;
        RandomLink = randomLink;
        Date = date;
    }

    public long AdminId { get; }
    public int Date { get; }
    public int? ExpireDate { get; }
    public bool LegacyRevokePermanent { get; }
    public string RandomLink { get; }
    public int? UsageLimit { get; }
}
