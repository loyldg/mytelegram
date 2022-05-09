namespace MyTelegram.Domain.CommandHandlers.Channel;

public class
    IncrementParticipantCountCommandHandler : CommandHandler<ChannelAggregate, ChannelId,
        IncrementParticipantCountCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        IncrementParticipantCountCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.IncrementParticipantCount();
        return Task.CompletedTask;
    }
}
