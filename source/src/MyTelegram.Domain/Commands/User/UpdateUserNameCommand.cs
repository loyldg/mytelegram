namespace MyTelegram.Domain.Commands.User;

public class UpdateUserNameCommand : RequestCommand<UserAggregate, UserId, IExecutionResult>, IHasCorrelationId
{
    public UpdateUserNameCommand(UserId aggregateId,
        long reqMsgId,
        string userName,
        Guid correlationId) : base(aggregateId, reqMsgId)
    {
        UserName = userName;
        CorrelationId = correlationId;
    }

    public string UserName { get; }

    public Guid CorrelationId { get; }
}
