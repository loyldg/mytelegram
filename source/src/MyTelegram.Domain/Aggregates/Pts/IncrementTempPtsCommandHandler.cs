namespace MyTelegram.Domain.Aggregates.Pts;

public class IncrementTempPtsCommandHandler : CommandHandler<TempPtsAggregate, TempPtsId, IncrementTempPtsCommand>
{
    public override Task ExecuteAsync(TempPtsAggregate aggregate, IncrementTempPtsCommand command, CancellationToken cancellationToken)
    {
        aggregate.IncrementPts(command.OwnerPeerId, command.NewPts, command.PermAuthKeyId);

        return Task.CompletedTask;
    }
}