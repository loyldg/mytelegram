namespace MyTelegram.Domain.CommandHandlers.Pts;

public class UpdateGlobalSeqNoCommandHandler : CommandHandler<PtsAggregate, PtsId, UpdateGlobalSeqNoCommand>
{
    public override Task ExecuteAsync(PtsAggregate aggregate,
        UpdateGlobalSeqNoCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UpdateGlobalSeqNo(command.PeerId, command.PermAuthKeyId, command.GlobalSeqNo);
        return Task.CompletedTask;
    }
}

//public class CreatePtsCommandHandler : CommandHandler<PtsAggregate, PtsId, CreatePtsCommand>
//{
//    public override Task ExecuteAsync(PtsAggregate aggregate,
//        CreatePtsCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.Create(command.PeerId, command.Pts, command.Qts, command.UnreadCount, command.Date, command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}

//public class IncrementPtsCommandHandler : CommandHandler<PtsAggregate, PtsId, IncrementPtsCommand>
//{
//    public override Task ExecuteAsync(PtsAggregate aggregate,
//        IncrementPtsCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.IncrementPts(command.Reason, command.MessageBoxId, command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}

//public class IncrementQtsCommandHandler : CommandHandler<PtsAggregate, PtsId, IncrementQtsCommand>
//{
//    public override Task ExecuteAsync(PtsAggregate aggregate,
//        IncrementQtsCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.IncrementQts(command.EncryptedMessageBoxId, command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}
