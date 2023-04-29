namespace MyTelegram.Domain.Events;

public abstract class RequestAggregateEvent2<TAggregate, TIdentity> : AggregateEvent<TAggregate, TIdentity>,
    IHasRequestInfo
    where TIdentity : IIdentity where TAggregate : IAggregateRoot<TIdentity>
{
    protected RequestAggregateEvent2(RequestInfo requestInfo)
    {
        RequestInfo = requestInfo;
    }

    public RequestInfo RequestInfo { get; }
}
