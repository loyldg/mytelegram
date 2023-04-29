namespace MyTelegram.Domain.Commands.User;

public class CheckUserStatusCommand : RequestCommand<UserAggregate, UserId, IExecutionResult>, IHasCorrelationId
{
    public CheckUserStatusCommand(UserId aggregateId,
        long reqMsgId,
        Guid correlationId) : base(aggregateId, reqMsgId)
    {
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
}

public class CheckUserStateCommand : Command<UserAggregate, UserId, IExecutionResult>, IHasCorrelationId
{
    public CheckUserStateCommand(UserId aggregateId,
        long reqMsgId,
        Guid correlationId) : base(aggregateId)
    {
        ReqMsgId = reqMsgId;
        CorrelationId = correlationId;
    }

    public long ReqMsgId { get; }
    public Guid CorrelationId { get; }
}
