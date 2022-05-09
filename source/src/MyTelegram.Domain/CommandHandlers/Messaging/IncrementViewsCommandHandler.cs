namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class IncrementViewsCommandHandler : CommandHandler<MessageAggregate, MessageId, IncrementViewsCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        IncrementViewsCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.IncrementViews();
        return Task.CompletedTask;
    }
}