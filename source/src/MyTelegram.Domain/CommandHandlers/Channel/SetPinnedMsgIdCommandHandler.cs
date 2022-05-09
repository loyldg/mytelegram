namespace MyTelegram.Domain.CommandHandlers.Channel;

public class SetPinnedMsgIdCommandHandler : CommandHandler<ChannelAggregate, ChannelId, SetPinnedMsgIdCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        SetPinnedMsgIdCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.SetPinnedMsgId(command.PinnedMsgId, command.Pinned);
        return Task.CompletedTask;
    }
}
