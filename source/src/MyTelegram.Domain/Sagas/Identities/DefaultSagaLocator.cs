namespace MyTelegram.Domain.Sagas.Identities;

//public class MySagaId : SingleValueObject<string>, ISagaId
//{
//    public MySagaId(string value) : base(value)
//    {
//    }
//}

//public abstract class DefaultSagaLocator : ISagaLocator
//{
//    public virtual Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
//    {
//        return Task.FromResult(CreateSagaId(Guid.NewGuid().ToString()));
//    }

//    //protected abstract TSagaId CreateSagaId(string requestId);
//    protected virtual ISagaId CreateSagaId(string requestId)
//    {
//        return new MySagaId(requestId);
//    }

//}


//public class DefaultSagaLocator<TSaga> : DefaultSagaLocator
//    where TSaga : class
//{
//    protected override ISagaId CreateSagaId(string requestId)
//    {
//        return new MySagaId($"{typeof(TSaga).Name.ToLower()}-{GuidFactories.Deterministic.Create(GuidFactories.Deterministic.Namespaces.Commands, requestId)}");
//    }
//}

public abstract class DefaultSagaLocator<TSagaId> : ISagaLocator
where TSagaId : ISagaId
{
    public virtual Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        if (domainEvent.GetAggregateEvent() is not IHasRequestInfo requestInfo)
        {
            throw new NotSupportedException(
                $"{domainEvent.GetAggregateEvent().GetType().FullName} must implement the IHasRequestInfo interface");
        }

        return Task.FromResult<ISagaId>(CreateSagaId($"saga-{requestInfo.RequestInfo.RequestId}"));
    }

    protected abstract TSagaId CreateSagaId(string requestId);
}

//public class MySagaId : SingleValueObject<string>, ISagaId
//{
//    public MySagaId(string value) : base(value)
//    {
//    }
//}

public abstract class DefaultSagaLocator<TSaga, TSagaId> : DefaultSagaLocator<TSagaId>
    where TSaga : class
where TSagaId : ISagaId
{
    public override Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        if (domainEvent.GetAggregateEvent() is not IHasRequestInfo requestInfo)
        {
            throw new NotSupportedException(
                $"{domainEvent.GetAggregateEvent().GetType().FullName} must implement the IHasRequestInfo interface");
        }
        return Task.FromResult<ISagaId>(CreateSagaId($"{typeof(TSaga).Name.ToLower()}-{requestInfo.RequestInfo.RequestId}"));
    }
}