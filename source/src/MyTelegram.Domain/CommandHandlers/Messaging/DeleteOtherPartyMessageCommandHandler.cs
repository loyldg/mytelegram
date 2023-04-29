namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class
    DeleteOtherPartyMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, DeleteOtherPartyMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        DeleteOtherPartyMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.DeleteOtherPartyMessage(command.CorrelationId);
        return Task.CompletedTask;
    }
}
