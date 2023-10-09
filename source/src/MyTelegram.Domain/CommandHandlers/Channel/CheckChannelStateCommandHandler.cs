namespace MyTelegram.Domain.CommandHandlers.Channel;

public class CheckChannelStateCommandHandler : CommandHandler<ChannelAggregate, ChannelId, CheckChannelStateCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        CheckChannelStateCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CheckChannelState(command.RequestInfo, command.SenderPeerId, command.MessageId, command.Date, command.MessageSubType);
        return Task.CompletedTask;
    }
}
