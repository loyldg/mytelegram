namespace MyTelegram.Domain.Commands.User;

public class CheckUserStatusCommand : RequestCommand2<UserAggregate, UserId, IExecutionResult>
{
    public CheckUserStatusCommand(UserId aggregateId, RequestInfo requestInfo) : base(aggregateId, requestInfo)
    {
    }
}