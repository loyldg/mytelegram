using MyTelegram.Domain.Aggregates.Poll;
using MyTelegram.Domain.Commands.Poll;

namespace MyTelegram.Domain.CommandHandlers.Poll;

public class VoteCommandHandler : CommandHandler<PollAggregate, PollId, VoteCommand>
{
    public override Task ExecuteAsync(PollAggregate aggregate,
        VoteCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.Vote(command.Request, command.VoteUserPeerId, command.Options, command.CorrelationId);

        return Task.CompletedTask;
    }
}