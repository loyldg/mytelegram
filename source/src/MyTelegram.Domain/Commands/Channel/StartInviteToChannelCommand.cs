namespace MyTelegram.Domain.Commands.Channel;

public class StartInviteToChannelCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>
{
    public StartInviteToChannelCommand(ChannelId aggregateId,
        RequestInfo requestInfo,
        long channelId,
        long inviterId,
        int maxMessageId,
        IReadOnlyList<long> memberUserIdList,
        IReadOnlyList<long>? privacyRestrictedUserIds,
        IReadOnlyList<long> botUserIdList,
        int date,
        long randomId,
        string messageActionData,
        ChatJoinType chatJoinType
        ) : base(aggregateId, requestInfo)
    {
        ChannelId = channelId;
        InviterId = inviterId;
        MaxMessageId = maxMessageId;
        MemberUserIdList = memberUserIdList;
        PrivacyRestrictedUserIds = privacyRestrictedUserIds;
        BotUserIdList = botUserIdList;
        Date = date;
        RandomId = randomId;
        MessageActionData = messageActionData;
        ChatJoinType = chatJoinType;
    }

    public IReadOnlyList<long> BotUserIdList { get; }
    public long ChannelId { get; }
    public int Date { get; }
    public long InviterId { get; }
    public int MaxMessageId { get; }
    public IReadOnlyList<long> MemberUserIdList { get; }
    public IReadOnlyList<long>? PrivacyRestrictedUserIds { get; }
    public string MessageActionData { get; }
    public ChatJoinType ChatJoinType { get; }
    public long RandomId { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(RequestInfo.ReqMsgId);
        yield return RequestInfo.RequestId.ToByteArray();
    }
}
