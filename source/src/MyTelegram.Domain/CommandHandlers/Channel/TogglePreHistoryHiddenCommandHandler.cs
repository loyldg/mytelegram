namespace MyTelegram.Domain.CommandHandlers.Channel;

public class
    TogglePreHistoryHiddenCommandHandler : CommandHandler<ChannelAggregate, ChannelId,
        TogglePreHistoryHiddenCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        TogglePreHistoryHiddenCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.TogglePreHistoryHidden(command.RequestInfo, command.Hidden, command.SelfUserId);
        return Task.CompletedTask;
    }
}