namespace MyTelegram.Domain;

public abstract class RequestCommand2<TAggregate, TIdentity, TExecutionResult>(
    TIdentity aggregateId,
    RequestInfo requestInfo) : DistinctCommand<TAggregate, TIdentity, TExecutionResult>(aggregateId), IHasRequestInfo
    where TIdentity : IIdentity
    where TAggregate : IAggregateRoot<TIdentity>
    where TExecutionResult : IExecutionResult
{
    public RequestInfo RequestInfo { get; } = requestInfo;

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(RequestInfo.ReqMsgId);
    }
}