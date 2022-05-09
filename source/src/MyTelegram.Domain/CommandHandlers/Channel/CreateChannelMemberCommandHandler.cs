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
            command.ChannelId,
            command.UserId,
            command.InviterId,
            command.Date,
            command.IsBot,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
