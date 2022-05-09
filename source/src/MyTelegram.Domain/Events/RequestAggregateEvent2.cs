namespace MyTelegram.Domain.Events;

public abstract class RequestAggregateEvent2<TAggregate, TIdentity> : AggregateEvent<TAggregate, TIdentity>, IHasRequestInfo
    where TIdentity : IIdentity where TAggregate : IAggregateRoot<TIdentity>
{
    public RequestInfo Request { get; }

    protected RequestAggregateEvent2(RequestInfo request)
    {
        Request = request;
    }
}
