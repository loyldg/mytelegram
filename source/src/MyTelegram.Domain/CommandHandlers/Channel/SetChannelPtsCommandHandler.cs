namespace MyTelegram.Domain.CommandHandlers.Channel;

public class SetChannelPtsCommandHandler : CommandHandler<ChannelAggregate, ChannelId, SetChannelPtsCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        SetChannelPtsCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.SetPts(command.SenderPeerId, command.Pts, command.MessageId, command.Date);
        return Task.CompletedTask;
    }
}
