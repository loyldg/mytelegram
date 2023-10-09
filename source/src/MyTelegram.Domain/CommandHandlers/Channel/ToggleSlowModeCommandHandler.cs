namespace MyTelegram.Domain.CommandHandlers.Channel;

public class ToggleSlowModeCommandHandler : CommandHandler<ChannelAggregate, ChannelId, ToggleSlowModeCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        ToggleSlowModeCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.ToggleSlowMode(command.RequestInfo, command.Seconds, command.SelfUserId);
        return Task.CompletedTask;
    }
}