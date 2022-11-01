namespace MyTelegram.Domain.Events;

public abstract class RequestAggregateEvent2<TAggregate, TIdentity> : AggregateEvent<TAggregate, TIdentity>, IHasRequestInfo
    where TIdentity : IIdentity where TAggregate : IAggregateRoot<TIdentity>
{
    public RequestInfo RequestInfo { get; }

    protected RequestAggregateEvent2(RequestInfo requestInfo)
    {
        RequestInfo = requestInfo;
    }
}
