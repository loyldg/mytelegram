namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class StartSendMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, StartSendMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        StartSendMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartSendMessage(command.Request, command.OutMessageItem, command.ClearDraft, command.GroupItemCount, command.CorrelationId);
        return Task.CompletedTask;
    }
}