using System.Collections.Concurrent;
using System.Reflection;

namespace MyTelegram.Domain.EventFlow;

public class MyDomainEventFactory : IDomainEventFactory
{
    private static readonly ConcurrentDictionary<Type, Type> AggregateEventToDomainEventTypeMap = new();
    private static readonly ConcurrentDictionary<Type, Type> DomainEventToIdentityTypeMap = new();
    private static readonly
        ConcurrentDictionary<Type, Func<IAggregateEvent, IMetadata, DateTimeOffset, IIdentity, int, IDomainEvent>>
        DomainEventTypeToCreateInstanceFuncMap = new();

    private static readonly ConcurrentDictionary<Type, Func<string, IIdentity>> IdentityTypeToCreateInstanceFuncMap = new();
    public IDomainEvent Create(
        IAggregateEvent aggregateEvent,
        IMetadata metadata,
        string aggregateIdentity,
        int aggregateSequenceNumber)
    {
        var domainEventType = AggregateEventToDomainEventTypeMap.GetOrAdd(aggregateEvent.GetType(), GetDomainEventType);
        var identityType = DomainEventToIdentityTypeMap.GetOrAdd(domainEventType, GetIdentityType);
        //var identity = Activator.CreateInstance(identityType, aggregateIdentity);
        if (!IdentityTypeToCreateInstanceFuncMap.TryGetValue(identityType, out var createInstanceFunc))
        {
            createInstanceFunc = MyReflectionHelper.CompileConstructor<string, IIdentity>(identityType);
            IdentityTypeToCreateInstanceFuncMap.TryAdd(identityType, createInstanceFunc);
        }
        IIdentity identity = createInstanceFunc(aggregateIdentity);
        if (!DomainEventTypeToCreateInstanceFuncMap.TryGetValue(domainEventType, out var createDomainEventInstanceFunc))
        {
            createDomainEventInstanceFunc =
                MyReflectionHelper.CompileConstructor<IAggregateEvent, IMetadata, DateTimeOffset, IIdentity, int, IDomainEvent>(
                    aggregateEvent.GetType(),
                    typeOfT4Impl: identityType,
                    typeOfTResultImpl: domainEventType
                );
            DomainEventTypeToCreateInstanceFuncMap.TryAdd(domainEventType, createDomainEventInstanceFunc);
        }

        return createDomainEventInstanceFunc(aggregateEvent,
            metadata,
            metadata.Timestamp,
            identity,
            aggregateSequenceNumber);
    }

    public IDomainEvent<TAggregate, TIdentity> Create<TAggregate, TIdentity>(
        IAggregateEvent aggregateEvent,
        IMetadata metadata,
        TIdentity id,
        int aggregateSequenceNumber)
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
    {
        return (IDomainEvent<TAggregate, TIdentity>)Create(
            aggregateEvent,
            metadata,
            id.Value,
            aggregateSequenceNumber);
    }

    public IDomainEvent<TAggregate, TIdentity> Upgrade<TAggregate, TIdentity>(
        IDomainEvent domainEvent,
        IAggregateEvent aggregateEvent)
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
    {
        return Create<TAggregate, TIdentity>(
            aggregateEvent,
            domainEvent.Metadata,
            (TIdentity)domainEvent.GetIdentity(),
            domainEvent.AggregateSequenceNumber);
    }

    private static Type GetIdentityType(Type domainEventType)
    {
        var domainEventInterfaceType = domainEventType
            .GetTypeInfo()
            .GetInterfaces()
            .SingleOrDefault(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IDomainEvent<,>));

        if (domainEventInterfaceType == null)
        {
            throw new ArgumentException($"Type '{domainEventType.PrettyPrint()}' is not a '{typeof(IDomainEvent<,>).PrettyPrint()}'");
        }

        var genericArguments = domainEventInterfaceType.GetTypeInfo().GetGenericArguments();
        return genericArguments[1];
    }

    private static Type GetDomainEventType(Type aggregateEventType)
    {
        var aggregateEventInterfaceType = aggregateEventType
            .GetTypeInfo()
            .GetInterfaces()
            .SingleOrDefault(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IAggregateEvent<,>));

        if (aggregateEventInterfaceType == null)
        {
            throw new ArgumentException($"Type '{aggregateEventType.PrettyPrint()}' is not a '{typeof(IAggregateEvent<,>).PrettyPrint()}'");
        }

        var genericArguments = aggregateEventInterfaceType.GetTypeInfo().GetGenericArguments();
        return typeof(DomainEvent<,,>).MakeGenericType(genericArguments[0], genericArguments[1], aggregateEventType);
    }
}