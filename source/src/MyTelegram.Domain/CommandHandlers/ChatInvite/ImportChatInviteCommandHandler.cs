using MyTelegram.Domain.Aggregates.ChatInvite;
using MyTelegram.Domain.Commands.ChatInvite;

namespace MyTelegram.Domain.CommandHandlers.ChatInvite;

public class ImportChatInviteCommandHandler : CommandHandler<ChatInviteAggregate, ChatInviteId, ImportChatInviteCommand>
{
    public override Task ExecuteAsync(ChatInviteAggregate aggregate, ImportChatInviteCommand command, CancellationToken cancellationToken)
    {
        aggregate.ImportChatInvite(command.RequestInfo, command.ChatInviteRequestState, command.Date);

        return Task.CompletedTask;
    }
}