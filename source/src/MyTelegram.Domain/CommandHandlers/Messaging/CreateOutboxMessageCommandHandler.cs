namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class CreateOutboxMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, CreateOutboxMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        CreateOutboxMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CreateOutboxMessage(command.RequestInfo,
            command.OutboxMessageItem,
            command.MentionedUserIds,
            command.ReplyToMsgItems,
            command.ClearDraft,
            command.GroupItemCount,
            command.LinkedChannelId,
            command.ChatMembers
        );
        return Task.CompletedTask;
    }
}