namespace MyTelegram.Domain.CommandHandlers.Pts;

public class PtsAckedCommandHandler : CommandHandler<PtsAggregate, PtsId, PtsAckedCommand>
{
    public override Task ExecuteAsync(PtsAggregate aggregate,
        PtsAckedCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.PtsAcked(command.PeerId,
            command.PermAuthKeyId,
            command.MsgId,
            command.Pts,
            command.GlobalSeqNo,
            command.ToPeer);
        return Task.CompletedTask;
    }
}
