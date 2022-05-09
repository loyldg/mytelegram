namespace MyTelegram.Domain.Commands.Channel;

public class EditBannedCommand : RequestCommand<ChannelMemberAggregate, ChannelMemberId, IExecutionResult>
{
    public EditBannedCommand(ChannelMemberId aggregateId,
        long reqMsgId,
        long adminId,
        long channelId,
        long memberUid,
        ChatBannedRights chatBannedRights) : base(aggregateId, reqMsgId)
    {
        AdminId = adminId;
        ChannelId = channelId;
        MemberUid = memberUid;
        ChatBannedRights = chatBannedRights;
    }

    public long AdminId { get; }
    public long ChannelId { get; }
    public ChatBannedRights ChatBannedRights { get; }
    public long MemberUid { get; }
}
