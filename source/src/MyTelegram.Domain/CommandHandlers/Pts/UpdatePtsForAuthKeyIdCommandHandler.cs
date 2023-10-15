namespace MyTelegram.Domain.CommandHandlers.Pts;

public class UpdatePtsForAuthKeyIdCommandHandler : CommandHandler<PtsAggregate, PtsId, UpdatePtsForAuthKeyIdCommand>
{
    public override Task ExecuteAsync(PtsAggregate aggregate, UpdatePtsForAuthKeyIdCommand command, CancellationToken cancellationToken)
    {
        aggregate.UpdatePtsForAuthKeyId(command.PeerId, command.PermAuthKeyId, command.NewPts, command.ChangedUnreadCount, command.GlobalSeqNo);
        return Task.CompletedTask;
    }
}