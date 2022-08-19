using MyTelegram.Domain.Aggregates.Poll;
using MyTelegram.Domain.Commands.Poll;

namespace MyTelegram.Domain.CommandHandlers.Poll;

public class DeleteVoteAnswerCommandHandler : CommandHandler<PollAggregate, PollId, DeleteVoteAnswerCommand>
{
    public override Task ExecuteAsync(PollAggregate aggregate,
        DeleteVoteAnswerCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.DeleteVoteAnswer(command.PollId, command.VoterPeerId);
        return Task.CompletedTask;
    }
}