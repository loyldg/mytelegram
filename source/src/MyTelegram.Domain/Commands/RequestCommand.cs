namespace MyTelegram.Domain.Commands;

public abstract class RequestCommand<TAggregate, TIdentity, TExecutionResult> :
    DistinctCommand<TAggregate, TIdentity, TExecutionResult>,
    IHasRequestMessageId
    where TExecutionResult : IExecutionResult where TIdentity : IIdentity where TAggregate : IAggregateRoot<TIdentity>
{
    protected RequestCommand(TIdentity aggregateId,
        long reqMsgId) : base(aggregateId)
    {
        ReqMsgId = reqMsgId;
    }
    //protected RequestCommand(TIdentity aggregateId, long reqMsgId) : base(aggregateId)
    //{
    //    ReqMsgId = reqMsgId;
    //}

    public long ReqMsgId { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(ReqMsgId);
    }
}