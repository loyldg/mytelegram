namespace MyTelegram.Domain.Commands.UserName;

public class DeleteUserNameCommand : Command<UserNameAggregate, UserNameId, IExecutionResult>
{
    public DeleteUserNameCommand(UserNameId aggregateId) : base(aggregateId)
    {
    }

    public DeleteUserNameCommand(UserNameId aggregateId,
        ISourceId sourceId) : base(aggregateId, sourceId)
    {
    }
}
