namespace MyTelegram.Domain.CommandHandlers.Channel;

public class
    CreateChannelMemberCommandHandler : CommandHandler<ChannelMemberAggregate, ChannelMemberId,
        CreateChannelMemberCommand>
{
    public override Task ExecuteAsync(ChannelMemberAggregate aggregate,
        CreateChannelMemberCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.Create( //command.ReqMsgId,
            command.RequestInfo,
            command.ChannelId,
            command.UserId,
            command.InviterId,
            command.Date,
            command.IsBot,
            command.ChatInviteId);
        return Task.CompletedTask;
    }
}