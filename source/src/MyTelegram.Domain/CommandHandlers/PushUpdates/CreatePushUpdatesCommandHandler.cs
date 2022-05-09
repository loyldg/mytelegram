namespace MyTelegram.Domain.CommandHandlers.PushUpdates;

public class
    CreatePushUpdatesCommandHandler : CommandHandler<PushUpdatesAggregate, PushUpdatesId, CreatePushUpdatesCommand>
{
    public override Task ExecuteAsync(PushUpdatesAggregate aggregate,
        CreatePushUpdatesCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.Create(command.ToPeer,
            command.ExcludeAuthKeyId,
            command.ExcludeUid,
            command.OnlySendToThisAuthKeyId,
            command.Data,
            command.Pts,
            command.PtsType,
            command.SeqNo);
        return Task.CompletedTask;
    }
}
