namespace MyTelegram.Domain.CommandHandlers.Channel;

public class CreateChannelCreatorMemberCommandHandler : CommandHandler<ChannelMemberAggregate, ChannelMemberId,
    CreateChannelCreatorMemberCommand>
{
    public override Task ExecuteAsync(ChannelMemberAggregate aggregate,
        CreateChannelCreatorMemberCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CreateCreator(command.ReqMsgId, command.ChannelId, command.UserId, command.Date);
        return Task.CompletedTask;
    }
}
