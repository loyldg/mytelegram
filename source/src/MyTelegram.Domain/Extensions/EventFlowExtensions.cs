namespace MyTelegram.Domain.Extensions;

public static class EventFlowExtensions
{
    public static IServiceCollection AddInMemorySnapshotAggregateServices(this IServiceCollection services)
    {
        //services.AddTransient<ISnapshotStore, SnapshotWithInMemoryCacheStore>();
        //services.AddSingleton<IMyInMemorySnapshotPersistence, MyInMemorySnapshotPersistence>();
        //services.AddTransient<IDispatchToSagas, MyDispatchToSagas>();
        //services.AddSingleton<IInMemorySagaStore, InMemorySagaStore>();

        return services;
    }
}
