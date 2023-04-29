using System.Collections.Concurrent;
using EventFlow;

namespace MyTelegram.Domain.Sagas;

public class MyDispatchToSagas : IDispatchToSagas
{
    private readonly ILogger<MyDispatchToSagas> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ISagaStore _sagaStore;
    private readonly IInMemorySagaStore _inMemorySagaStore;
    private readonly ISagaDefinitionService _sagaDefinitionService;
    private readonly ISagaErrorHandler _sagaErrorHandler;
    private readonly ISagaUpdateResilienceStrategy _sagaUpdateLog;
    private readonly Func<Type, ISagaErrorHandler> _sagaErrorHandlerFactory;

    public MyDispatchToSagas(
        ILogger<MyDispatchToSagas> logger,
        IServiceProvider serviceProvider,
        ISagaStore sagaStore,
        ISagaDefinitionService sagaDefinitionService,
        ISagaErrorHandler sagaErrorHandler,
        ISagaUpdateResilienceStrategy sagaUpdateLog,
        Func<Type, ISagaErrorHandler> sagaErrorHandlerFactory,
        IInMemorySagaStore inMemorySagaStore)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _sagaStore = sagaStore;
        _sagaDefinitionService = sagaDefinitionService;
        _sagaErrorHandler = sagaErrorHandler;
        _sagaUpdateLog = sagaUpdateLog;
        _sagaErrorHandlerFactory = sagaErrorHandlerFactory;
        _inMemorySagaStore = inMemorySagaStore;
    }

    public async Task ProcessAsync(
        IReadOnlyCollection<IDomainEvent> domainEvents,
        CancellationToken cancellationToken)
    {
        foreach (var domainEvent in domainEvents)
        {
            await ProcessAsync(
                    domainEvent,
                    cancellationToken)
                ;
        }
    }

    private async Task ProcessAsync(
        IDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        var sagaTypeDetails = _sagaDefinitionService.GetSagaDetails(domainEvent.EventType);

        if (_logger.IsEnabled(LogLevel.Trace))
        {
            _logger.LogTrace(
                "Saga types to process for domain event {DomainEventType}: {SagaTypes}",
                domainEvent.EventType.PrettyPrint(),
                sagaTypeDetails.Select(d => d.SagaType.PrettyPrint()));
        }

        foreach (var details in sagaTypeDetails)
        {
            var locator = (ISagaLocator)_serviceProvider.GetRequiredService(details.SagaLocatorType);
            var sagaId = await locator.LocateSagaAsync(domainEvent, cancellationToken);

            if (sagaId == null)
            {
                _logger.LogTrace(
                    "Saga locator {SagaLocatorType} returned null",
                    details.SagaLocatorType.PrettyPrint());
                continue;
            }

            await ProcessSagaAsync(domainEvent, sagaId, details, cancellationToken);
        }
    }

    private async Task ProcessSagaAsync(
        IDomainEvent domainEvent,
        ISagaId sagaId,
        SagaDetails details,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogTrace(
                "Loading saga {SagaType} with ID {Id}",
                details.SagaType.PrettyPrint(),
                sagaId);

            if (typeof(IInMemorySaga).IsAssignableFrom(details.SagaType))
            {
                await _inMemorySagaStore.UpdateAsync(
                        sagaId,
                        details.SagaType,
                        domainEvent.Metadata.EventId,
                        (s,
                            c) => UpdateSagaAsync(s, domainEvent, details, c),
                        cancellationToken)
                    ;
            }
            else
            {
                await _sagaStore.UpdateAsync(
                        sagaId,
                        details.SagaType,
                        domainEvent.Metadata.EventId,
                        (s,
                            c) => UpdateSagaAsync(s, domainEvent, details, c),
                        cancellationToken)
                    ;
            }
        }
        catch (Exception e)
        {
            // Search for a specific SagaErrorHandler<Saga> based on saga type
            ISagaErrorHandler specificSagaErrorHandler = _sagaErrorHandlerFactory(details.SagaType);

            bool handled = specificSagaErrorHandler != null ?
                await specificSagaErrorHandler.HandleAsync(sagaId, details, e, cancellationToken).ConfigureAwait(false) :
                await _sagaErrorHandler.HandleAsync(sagaId, details, e, cancellationToken);

            if (handled)
            {
                return;
            }

            _logger.LogError(
                "Failed to process domain event {DomainEventType} for saga {SagaType}",
                domainEvent.EventType,
                details.SagaType.PrettyPrint());
            throw;
        }
    }

    private async Task UpdateSagaAsync(
        ISaga saga,
        IDomainEvent domainEvent,
        SagaDetails details,
        CancellationToken cancellationToken)
    {
        if (saga.State == SagaState.Completed)
        {
            _logger.LogTrace(
                "Saga {SagaType} is completed, skipping processing of {DomainEventType}",
                details.SagaType.PrettyPrint(),
                domainEvent.EventType.PrettyPrint());
            return;
        }

        if (saga.State == SagaState.New && !details.IsStartedBy(domainEvent.EventType))
        {
            _logger.LogTrace(
                "Saga {SagaType} isn't started yet and not started by {DomainEventType}, skipping",
                details.SagaType.PrettyPrint(),
                domainEvent.EventType.PrettyPrint());
            return;
        }

        var sagaUpdaterType = typeof(ISagaUpdater<,,,>).MakeGenericType(
            domainEvent.AggregateType,
            domainEvent.IdentityType,
            domainEvent.EventType,
            details.SagaType);
        var sagaUpdater = (ISagaUpdater)_serviceProvider.GetRequiredService(sagaUpdaterType);

        await _sagaUpdateLog.BeforeUpdateAsync(
                saga,
                domainEvent,
                details,
                cancellationToken)
            ;
        try
        {
            await sagaUpdater.ProcessAsync(
                    saga,
                    domainEvent,
                    SagaContext.Empty,
                    cancellationToken)
                ;
            await _sagaUpdateLog.UpdateSucceededAsync(
                    saga,
                    domainEvent,
                    details,
                    cancellationToken)
                ;
        }
        catch (Exception e)
        {
            if (!await _sagaUpdateLog.HandleUpdateFailedAsync(
                        saga,
                        domainEvent,
                        details,
                        e,
                        cancellationToken)
                    .ConfigureAwait(false))
            {
                throw;
            }
        }
    }
}

public interface IInMemorySaga
{
}
public interface IInMemorySagaStore : ISagaStore
{
}
public class InMemorySagaStore : SagaStore, IInMemorySagaStore
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentDictionary<ISagaId, ISaga> _sagas = new();
    //private readonly AsyncLock _asyncLock = new();
    private readonly ConcurrentDictionary<Type, Func<ISagaId, ISaga>> _sagaCreators = new();

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
