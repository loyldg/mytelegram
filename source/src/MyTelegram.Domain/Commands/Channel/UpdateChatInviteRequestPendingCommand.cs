namespace MyTelegram.Domain.Commands.Channel;

public class UpdateChatInviteRequestPendingCommand : Command<ChannelAggregate, ChannelId, IExecutionResult>
{
    public long RequestUserId { get; }

    public UpdateChatInviteRequestPendingCommand(ChannelId aggregateId, long requestUserId) : base(aggregateId)
    {
        RequestUserId = requestUserId;
    }
}