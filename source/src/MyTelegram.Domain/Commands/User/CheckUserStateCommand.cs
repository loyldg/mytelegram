namespace MyTelegram.Domain.Commands.User;

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