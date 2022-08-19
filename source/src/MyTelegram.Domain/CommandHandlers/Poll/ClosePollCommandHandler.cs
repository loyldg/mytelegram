namespace MyTelegram.Domain.CommandHandlers.Poll;

public class ClosePollCommandHandler : CommandHandler<PollAggregate, PollId, ClosePollCommand>
{
    public override Task ExecuteAsync(PollAggregate aggregate,
        ClosePollCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.Close(command.CloseDate);

        return Task.CompletedTask;
    }
}
