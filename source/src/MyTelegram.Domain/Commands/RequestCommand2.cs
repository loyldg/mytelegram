namespace MyTelegram.Domain.Commands;

public abstract class RequestCommand2<TAggregate, TIdentity, TExecutionResult> : DistinctCommand<TAggregate, TIdentity, TExecutionResult>
    where TIdentity : IIdentity where TAggregate : IAggregateRoot<TIdentity> where TExecutionResult : IExecutionResult
{
    public RequestInfo Request { get; }

    protected RequestCommand2(TIdentity aggregateId, RequestInfo request) : base(aggregateId)
    {
        Request = request;
    }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(Request.ReqMsgId);
    }
}
