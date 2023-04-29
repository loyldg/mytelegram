using AutoFixture;
using AutoFixture.AutoMoq;
using EventFlow.Aggregates;
using EventFlow.Core;
using EventFlow.EventStores;
using Moq;

namespace MyTelegram.TestBase;

public abstract class MyTelegramTestBase
{
    protected MyTelegramTestBase()
    {
        Fixture = new Fixture().Customize(new AutoMoqCustomization());
        Fixture.Customize<EventId>(c => c.FromFactory(() => EventId.New));
        Fixture.Customize<Label>(s => s.FromFactory(() => Label.Named($"label-{Guid.NewGuid():D}")));

        DomainEventFactory = new DomainEventFactory();
    }

    protected IFixture Fixture { get; }
    protected IDomainEventFactory DomainEventFactory { get; }

    protected T A<T>()
    {
        return Fixture.Create<T>();
    }

    protected IDomainEvent<TAggregate, TIdentity, TAggregateEvent> ADomainEvent<TAggregate, TIdentity, TAggregateEvent>(
        TAggregateEvent aggregateEvent,
        TIdentity identity,
        int aggregateSequenceNumber = 0
    ) where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
        where TAggregateEvent : IAggregateEvent<TAggregate, TIdentity>
    {
        return new DomainEvent<TAggregate, TIdentity, TAggregateEvent>(aggregateEvent,
            A<Metadata>(),
            A<DateTimeOffset>(),
            //A<TIdentity>(),
            identity,
            aggregateSequenceNumber);
    }

    protected IDomainEvent<TAggregate, TIdentity> ADomainEvent<TAggregate, TIdentity, TAggregateEvent>(
        int aggregateSequenceNumber = 0
    ) where TAggregate : IAggregateRoot<TIdentity> where TIdentity : IIdentity where TAggregateEvent : IAggregateEvent
    {
        return ToDomainEvent<TAggregate, TIdentity, TAggregateEvent>(A<TIdentity>(),
            A<TAggregateEvent>(),
            aggregateSequenceNumber);
    }

    protected IDomainEvent<TAggregate, TIdentity> ADomainEvent<TAggregate, TIdentity, TAggregateEvent>(
        TAggregateEvent aggregateEvent,
        int aggregateSequenceNumber = 0
    ) where TAggregate : IAggregateRoot<TIdentity> where TIdentity : IIdentity where TAggregateEvent : IAggregateEvent
    {
        return ToDomainEvent<TAggregate, TIdentity, TAggregateEvent>(A<TIdentity>(),
            aggregateEvent,
            aggregateSequenceNumber);
    }

    protected T Inject<T>(T instance)
        where T : class
    {
        Fixture.Inject(instance);
        return instance;
    }

    protected Mock<T> InjectMock<T>(params object[] args) where T : class
    {
        var mock = new Mock<T>(args);
        Fixture.Inject(mock.Object);

        return mock;
    }

    protected List<T> Many<T>(int count = 3)
    {
        return Fixture.CreateMany<T>(count).ToList();
    }

    protected T Mock<T>() where T : class
    {
        return new Mock<T>().Object;
    }

    protected IDomainEvent<TAggregate, TIdentity> ToDomainEvent<TAggregate, TIdentity, TAggregateEvent>(
        TIdentity identity,
        TAggregateEvent aggregateEvent,
        int aggregateSequenceNumber = 0
    ) where TAggregate : IAggregateRoot<TIdentity> where TIdentity : IIdentity where TAggregateEvent : IAggregateEvent
    {
        var metadata = new Metadata
        {
            Timestamp = A<DateTimeOffset>(),
            SourceId = A<SourceId>(),
            EventId = A<EventId>()
        };
        if (aggregateSequenceNumber == 0)
        {
            aggregateSequenceNumber = A<int>();
        }

        return DomainEventFactory.Create<TAggregate, TIdentity>(aggregateEvent,
            metadata,
            identity,
            aggregateSequenceNumber);
    }
}
