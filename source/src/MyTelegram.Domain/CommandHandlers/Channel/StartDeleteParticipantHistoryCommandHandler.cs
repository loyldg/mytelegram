namespace MyTelegram.Domain.CommandHandlers.Channel;

public class
    StartDeleteParticipantHistoryCommandHandler : CommandHandler<ChannelAggregate, ChannelId,
        StartDeleteParticipantHistoryCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        StartDeleteParticipantHistoryCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartDeleteParticipantHistory(command.Request,command.MessageIds,command.CorrelationId);

        return Task.CompletedTask;
    }
}