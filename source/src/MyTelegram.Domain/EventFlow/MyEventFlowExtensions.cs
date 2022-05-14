using EventFlow.ReadStores;

namespace MyTelegram.Domain.EventFlow;
public static class MyEventFlowExtensions
{
    public static IServiceCollection AddMyEventFlow(this IServiceCollection services)
    {
        services.AddTransient<IDomainEventFactory, MyDomainEventFactory>();
        services.AddTransient(typeof(IReadModelFactory<>), typeof(MyReadModelFactory<>));
        services.AddTransient<IAggregateFactory, MyAggregateFactory>();
        services.AddTransient<IJsonSerializer, SystemTextJsonSerializer>();
        services.AddSingleton<IInMemoryEventPersistence, MyInMemoryEventPersistence>();

        services.AddTransient<IEventStore, MyEventStoreBase>();

        services.AddTransient<ISnapshotStore, SnapshotWithInMemoryCacheStore>();
        services.AddSingleton<IMyInMemorySnapshotPersistence, MyInMemorySnapshotPersistence>();

        return services;
    }
}