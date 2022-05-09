using EventFlow;
using EventFlow.Aggregates;
using EventFlow.EventStores;
using EventFlow.Extensions;
using EventFlow.Queries;
using EventFlow.ReadStores;
using EventFlow.Sagas;
using EventFlow.Snapshots;
using EventFlow.Snapshots.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MyTelegram.TestBase;

public abstract class IntegrationTest : MyTelegramTestBase,IDisposable
{
    protected IServiceProvider ServiceProvider { get; }
    protected IAggregateStore AggregateStore { get; }
    protected IEventStore EventStore { get; }
    protected ISnapshotStore SnapshotStore { get; }
    protected ISnapshotPersistence SnapshotPersistence { get; }
    protected ISnapshotDefinitionService SnapshotDefinitionService { get; }
    protected IEventPersistence EventPersistence { get; }
    protected IQueryProcessor QueryProcessor { get; }
    protected ICommandBus CommandBus { get; }
    protected ISagaStore SagaStore { get; }
    protected IReadModelPopulator ReadModelPopulator { get; }
    protected ILogger Logger { get; }

    protected IntegrationTest()
    {
        ServiceProvider = CreateServiceProvider();
        AggregateStore = ServiceProvider.GetRequiredService<IAggregateStore>();
        EventStore = ServiceProvider.GetRequiredService<IEventStore>();
        SnapshotStore = ServiceProvider.GetRequiredService<ISnapshotStore>();
        SnapshotPersistence = ServiceProvider.GetRequiredService<ISnapshotPersistence>();
        SnapshotDefinitionService = ServiceProvider.GetRequiredService<ISnapshotDefinitionService>();
        EventPersistence = ServiceProvider.GetRequiredService<IEventPersistence>();
        CommandBus = ServiceProvider.GetRequiredService<ICommandBus>();
        QueryProcessor = ServiceProvider.GetRequiredService<IQueryProcessor>();
        ReadModelPopulator = ServiceProvider.GetRequiredService<IReadModelPopulator>();
        SagaStore = ServiceProvider.GetRequiredService<ISagaStore>();
        Logger = ServiceProvider.GetRequiredService<ILogger<IntegrationTest>>();
    }

    private IServiceProvider CreateServiceProvider()
    {
        var eventFlowOptions = Options(EventFlowOptions.New())
                .AddDefaults(typeof(IntegrationTest).Assembly)
            ;
        return Configure(eventFlowOptions);
    }

    protected virtual IEventFlowOptions Options(IEventFlowOptions options) => options;

    protected virtual IServiceProvider Configure(IEventFlowOptions options) =>
        options.ServiceCollection.BuildServiceProvider();

    public void Dispose()
    {
        ((IDisposable)ServiceProvider).Dispose();
    }
}
