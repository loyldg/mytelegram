namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class DeleteSelfMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, DeleteSelfMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        DeleteSelfMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.DeleteSelfMessage(command.RequestInfo, command.MessageId);
        return Task.CompletedTask;
    }
}