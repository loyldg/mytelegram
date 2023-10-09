using MyTelegram.Domain.Aggregates.Updates;
using MyTelegram.Domain.Commands.Updates;

namespace MyTelegram.Domain.CommandHandlers.Updates;

public class CreateUpdatesCommandHandler : CommandHandler<UpdatesAggregate, UpdatesId, CreateUpdatesCommand>
{
    public override Task ExecuteAsync(UpdatesAggregate aggregate,
        CreateUpdatesCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.Create(command.OwnerPeerId, command.ExcludeAuthKeyId, command.ExcludeUserId,
            command.OnlySendToUserId,
            command.OnlySendToThisAuthKeyId,
            command.UpdatesType, command.Pts, command.MessageId, command.Date,
            command.SeqNo, command.Updates, command.Users, command.Chats);

        return Task.CompletedTask;
    }
}