namespace MyTelegram.Domain.Commands.Channel;

public class StartInviteToChannelCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>//,
    //IHasCorrelationId
{
    public StartInviteToChannelCommand(ChannelId aggregateId,
        RequestInfo requestInfo,
        long channelId,
        long inviterId,
        int maxMessageId,
        IReadOnlyList<long> memberUidList,
        IReadOnlyList<long>? privacyRestrictedUserIds,
        IReadOnlyList<long> botUidList,
        int date,
        long randomId,
        string messageActionData) : base(aggregateId, requestInfo)
    {
        ChannelId = channelId;
        InviterId = inviterId;
        MaxMessageId = maxMessageId;
        MemberUidList = memberUidList;
        PrivacyRestrictedUserIds = privacyRestrictedUserIds;
        BotUidList = botUidList;
        Date = date;
        RandomId = randomId;
        MessageActionData = messageActionData;
    }

    public IReadOnlyList<long> BotUidList { get; }
    public long ChannelId { get; }
    public int Date { get; }
    public long InviterId { get; }
    public int MaxMessageId { get; }
    public IReadOnlyList<long> MemberUidList { get; }
    public IReadOnlyList<long>? PrivacyRestrictedUserIds { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(RequestInfo.ReqMsgId);
        yield return RequestInfo.RequestId.ToByteArray();
    }
}