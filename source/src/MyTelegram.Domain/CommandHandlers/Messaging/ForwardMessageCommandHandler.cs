namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class ForwardMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, ForwardMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        ForwardMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.ForwardMessage(command.RequestInfo, command.RandomId);
        return Task.CompletedTask;
    }
}