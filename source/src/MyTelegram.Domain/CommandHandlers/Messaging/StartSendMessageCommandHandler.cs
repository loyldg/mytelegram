namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class StartSendMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, StartSendMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        StartSendMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartSendMessage(command.RequestInfo, command.OutMessageItem, command.MentionedUserIds, command.ClearDraft, command.GroupItemCount, command.ForwardFromLinkedChannel);
        return Task.CompletedTask;
    }
}