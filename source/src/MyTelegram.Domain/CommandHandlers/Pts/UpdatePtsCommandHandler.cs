namespace MyTelegram.Domain.CommandHandlers.Pts;

public class UpdatePtsCommandHandler : CommandHandler<PtsAggregate, PtsId, UpdatePtsCommand>
{
    public override Task ExecuteAsync(PtsAggregate aggregate,
        UpdatePtsCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UpdatePts(command.PeerId, command.PermAuthKeyId, command.NewPts);
        return Task.CompletedTask;
    }
}
