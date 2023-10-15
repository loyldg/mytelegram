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

//public class UpdateChannelPtsForUserCommandHandler : CommandHandler<ChannelPtsAggregate, ChannelPtsId, UpdateChannelPtsForUserCommand>
//{
//    public override Task ExecuteAsync(ChannelPtsAggregate aggregate, UpdateChannelPtsForUserCommand command, CancellationToken cancellationToken)
//    {
//        aggregate.UpdateChannelPtsForUser(command.UserId,command.ChannelId,command.Pts,command.GlobalSeqNo);

//        return Task.CompletedTask;
//    }
//}