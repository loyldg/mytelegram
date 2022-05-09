namespace MyTelegram.Domain.Commands.Channel;

public class StartInviteToChannelCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>,
    IHasCorrelationId
{
    public StartInviteToChannelCommand(ChannelId aggregateId,
        RequestInfo request,
        long channelId,
        long inviterId,
        IReadOnlyList<long> memberUidList,
        IReadOnlyList<long> botUidList,
        int date,
        long randomId,
        string messageActionData,
        Guid correlationId) : base(aggregateId, request)
    {
        ChannelId = channelId;
        InviterId = inviterId;
        MemberUidList = memberUidList;
        BotUidList = botUidList;
        Date = date;
        RandomId = randomId;
        MessageActionData = messageActionData;
        CorrelationId = correlationId;
    }

    public IReadOnlyList<long> BotUidList { get; }
    public long ChannelId { get; }
    public int Date { get; }
    public long InviterId { get; }
    public IReadOnlyList<long> MemberUidList { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }
    public Guid CorrelationId { get; }
}
