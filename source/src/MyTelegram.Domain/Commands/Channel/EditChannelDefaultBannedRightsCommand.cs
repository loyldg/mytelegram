namespace MyTelegram.Domain.Commands.Channel;

public class EditChannelDefaultBannedRightsCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>
{
    public EditChannelDefaultBannedRightsCommand(ChannelId aggregateId,
        RequestInfo requestInfo,
        ChatBannedRights chatBannedRights,
        long selfUserId
    ) : base(aggregateId, requestInfo)
    {
        ChatBannedRights = chatBannedRights;
        SelfUserId = selfUserId;
    }

    public ChatBannedRights ChatBannedRights { get; }
    public long SelfUserId { get; }
}