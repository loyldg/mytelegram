namespace MyTelegram.Domain.Commands.Channel;

public class EditChannelDefaultBannedRightsCommand : RequestCommand<ChannelAggregate, ChannelId, IExecutionResult>
{
    public EditChannelDefaultBannedRightsCommand(ChannelId aggregateId,
        long reqMsgId,
        ChatBannedRights chatBannedRights,
        long selfUserId
    ) : base(aggregateId, reqMsgId)
    {
        ChatBannedRights = chatBannedRights;
        SelfUserId = selfUserId;
    }

    public ChatBannedRights ChatBannedRights { get; }
    public long SelfUserId { get; }
}
