namespace MyTelegram.Domain.Commands.Channel;

public class EditBannedCommand : RequestCommand2<ChannelMemberAggregate, ChannelMemberId, IExecutionResult>
{
    public EditBannedCommand(ChannelMemberId aggregateId,
        RequestInfo requestInfo,
        long adminId,
        long channelId,
        long memberUserId,
        ChatBannedRights chatBannedRights) : base(aggregateId, requestInfo)
    {
        AdminId = adminId;
        ChannelId = channelId;
        MemberUserId = memberUserId;
        ChatBannedRights = chatBannedRights;
    }

    public long AdminId { get; }
    public long ChannelId { get; }
    public ChatBannedRights ChatBannedRights { get; }
    public long MemberUserId { get; }
}