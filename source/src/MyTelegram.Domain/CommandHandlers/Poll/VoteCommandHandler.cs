namespace MyTelegram.Domain.CommandHandlers.Poll;

public class VoteCommandHandler : CommandHandler<PollAggregate, PollId, VoteCommand>
{
    public override Task ExecuteAsync(PollAggregate aggregate,
        VoteCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.Vote(command.RequestInfo, command.VoteUserPeerId, command.Options);

        return Task.CompletedTask;
    }
}