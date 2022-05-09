namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class ForwardMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, ForwardMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        ForwardMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.ForwardMessage(command.Request, command.RandomId, command.CorrelationId);
        return Task.CompletedTask;
    }
}