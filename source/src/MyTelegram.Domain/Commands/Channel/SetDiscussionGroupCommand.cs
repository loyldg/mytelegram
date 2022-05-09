namespace MyTelegram.Domain.Commands.Channel;

public class SetDiscussionGroupCommand : RequestCommand<ChannelAggregate, ChannelId, IExecutionResult>
{
    public SetDiscussionGroupCommand(ChannelId aggregateId,
        long reqMsgId,
        long selfUserId,
        long broadcastChannelId,
        long groupChannelId) : base(aggregateId, reqMsgId)
    {
        SelfUserId = selfUserId;
        BroadcastChannelId = broadcastChannelId;
        GroupChannelId = groupChannelId;
    }

    public long BroadcastChannelId { get; }
    public long GroupChannelId { get; }

    public long SelfUserId { get; }
}
