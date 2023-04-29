namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class CreateOutboxMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, CreateOutboxMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        CreateOutboxMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CreateOutboxMessage(command.ReqMsgId,
            command.OutboxMessageItem,
            command.ClearDraft,
            command.GroupItemCount,
            command.LinkedChannelId,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
