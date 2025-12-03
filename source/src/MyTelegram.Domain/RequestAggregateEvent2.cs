namespace MyTelegram.Domain;

public abstract class RequestAggregateEvent2<TAggregate, TIdentity>(RequestInfo requestInfo)
    : AggregateEvent<TAggregate, TIdentity>, IHasRequestInfo
    where TIdentity : IIdentity
    where TAggregate : IAggregateRoot<TIdentity>
{
    public RequestInfo RequestInfo { get; } = requestInfo;
}
