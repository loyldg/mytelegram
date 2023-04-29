namespace MyTelegram.Domain.CommandHandlers.Poll;

public class CreateVoteAnswerCommandHandler : CommandHandler<PollAggregate, PollId, CreateVoteAnswerCommand>
{
    public override Task ExecuteAsync(PollAggregate aggregate,
        CreateVoteAnswerCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CreateVoteAnswer(command.PollId, command.VoterPeerId, command.Option, command.Correct);
        return Task.CompletedTask;
    }
}
