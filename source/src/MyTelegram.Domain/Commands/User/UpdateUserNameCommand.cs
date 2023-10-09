namespace MyTelegram.Domain.Commands.User;

public class UpdateUserNameCommand : RequestCommand2<UserAggregate, UserId, IExecutionResult>
{
    public UpdateUserNameCommand(UserId aggregateId,
        RequestInfo requestInfo,
        string userName) : base(aggregateId, requestInfo)
    {
        UserName = userName;
    }

    public string UserName { get; }
}