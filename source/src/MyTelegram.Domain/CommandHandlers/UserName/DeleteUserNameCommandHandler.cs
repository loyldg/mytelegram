namespace MyTelegram.Domain.CommandHandlers.UserName;

public class DeleteUserNameCommandHandler : CommandHandler<UserNameAggregate, UserNameId, DeleteUserNameCommand>
{
    public override Task ExecuteAsync(UserNameAggregate aggregate,
        DeleteUserNameCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.Delete();
        return Task.CompletedTask;
    }
}

//public class CheckUserNameCommandHandler : CommandHandler<UserNameAggregate, UserNameId, CheckUserNameCommand>
//{
//    public override Task ExecuteAsync(UserNameAggregate aggregate,
//        CheckUserNameCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.Check(command.ReqMsgId, command.UserName);

//        return Task.CompletedTask;
//    }
//}
