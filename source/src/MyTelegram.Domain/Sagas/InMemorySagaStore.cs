using System.Collections.Concurrent;
using EventFlow;

namespace MyTelegram.Domain.Sagas;

public class InMemorySagaStore : SagaStore, IInMemorySagaStore
{
    //private readonly AsyncLock _asyncLock = new();
    private readonly ConcurrentDictionary<Type, Func<ISagaId, ISaga>> _sagaCreators = new();
    private readonly ConcurrentDictionary<ISagaId, ISaga> _sagas = new();
    private readonly IServiceProvider _serviceProvider;

    public InMemorySagaStore(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public override async Task<ISaga> UpdateAsync(
        ISagaId sagaId,
        Type sagaType,
        ISourceId sourceId,
        Func<ISaga, CancellationToken, Task> updateSaga,
        CancellationToken cancellationToken)
    {
        var commandBus = _serviceProvider.GetRequiredService<ICommandBus>();

        if (!_sagas.TryGetValue(sagaId, out var saga))
        {
            if (!_sagaCreators.TryGetValue(sagaType, out var sagaCreator))
            {
                sagaCreator = MyReflectionHelper.CompileConstructor<ISagaId, ISaga>(sagaId.GetType(), sagaType);
                _sagaCreators.TryAdd(sagaType, sagaCreator);
            }

            saga = sagaCreator(sagaId);
            _sagas.TryAdd(sagaId, saga);
        }

        await updateSaga(saga, cancellationToken);

        await saga.PublishAsync(commandBus, cancellationToken);
        return saga;
    }
}