namespace MyTelegram.Domain.CommandHandlers.PushUpdates;

public class CreateEncryptedPushUpdatesCommandHandler : CommandHandler<PushUpdatesAggregate, PushUpdatesId,
    CreateEncryptedPushUpdatesCommand>
{
    public override Task ExecuteAsync(PushUpdatesAggregate aggregate,
        CreateEncryptedPushUpdatesCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CreateEncryptedPushUpdate(command.InboxOwnerPeerId,
            command.Data,
            command.Qts,
            command.InboxOwnerPermAuthKeyId);
        return Task.CompletedTask;
    }
}
