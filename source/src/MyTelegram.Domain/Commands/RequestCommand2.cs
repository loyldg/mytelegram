namespace MyTelegram.Domain.Commands;

public abstract class RequestCommand2<TAggregate, TIdentity, TExecutionResult> : DistinctCommand<TAggregate, TIdentity, TExecutionResult>, IHasRequestInfo
    where TIdentity : IIdentity where TAggregate : IAggregateRoot<TIdentity> where TExecutionResult : IExecutionResult
{
    public RequestInfo RequestInfo { get; }

    protected RequestCommand2(TIdentity aggregateId, RequestInfo requestInfo) : base(aggregateId)
    {
        RequestInfo = requestInfo;
    }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(RequestInfo.ReqMsgId);
    }
}