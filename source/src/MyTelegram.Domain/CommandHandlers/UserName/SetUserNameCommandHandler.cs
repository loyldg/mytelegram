namespace MyTelegram.Domain.CommandHandlers.UserName;

public class SetUserNameCommandHandler : CommandHandler<UserNameAggregate, UserNameId, SetUserNameCommand>
{
    public override Task ExecuteAsync(UserNameAggregate aggregate,
        SetUserNameCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.SetUserName(command.ReqMsgId,
            command.SelfUserId,
            command.PeerType,
            command.PeerId,
            command.UserName,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
