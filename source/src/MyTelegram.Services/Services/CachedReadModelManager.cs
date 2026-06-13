using System.Reflection;
using EventFlow.Extensions;
using EventFlow.ReadStores;

namespace MyTelegram.Services.Services;

public abstract class CachedReadModelManager<TReadModelInterface, TReadModel, TKey>(
    IReadModelDomainEventApplier readModelDomainEventApplier,
    IServiceProvider serviceProvider,
    IReadModelCacheHelper<TReadModelInterface, TKey> readModelCacheHelper) : ICachedReadModelManager

    where TReadModel : class, IReadModel
    where TReadModelInterface : IReadModel
{
    private static readonly Type StaticReadModelType = typeof(TReadModel);
    private static readonly HashSet<Type> AggregateEventTypes;

    static CachedReadModelManager()
    {
        var iAmReadModelForInterfaceTypes = StaticReadModelType
            .GetTypeInfo()
            .GetInterfaces()
            .Where(IsReadModelFor)
            .ToList();
        if (iAmReadModelForInterfaceTypes.Count == 0)
        {
            throw new ArgumentException(
                $"Read model type '{StaticReadModelType.PrettyPrint()}' does not implement any '{typeof(IAmReadModelFor<,,>).PrettyPrint()}'");
        }

        AggregateEventTypes = [.. iAmReadModelForInterfaceTypes.Select(i => i.GetTypeInfo().GetGenericArguments()[2])];
        if (AggregateEventTypes.Count != iAmReadModelForInterfaceTypes.Count)
        {
            throw new ArgumentException(
                $"Read model type '{StaticReadModelType.PrettyPrint()}' implements ambiguous '{typeof(IAmReadModelFor<,,>).PrettyPrint()}' interfaces");
        }
    }

    private static bool IsReadModelFor(Type i)
    {
        if (!i.GetTypeInfo().IsGenericType)
        {
            return false;
        }

        var typeDefinition = i.GetGenericTypeDefinition();
        return typeDefinition == typeof(IAmReadModelFor<,,>);
    }

    public async Task ApplyUpdatesAsync(IReadOnlyCollection<IDomainEvent> domainEvents,
        CancellationToken cancellationToken)
    {
        var relevantDomainEvents = domainEvents
            .Where(e => AggregateEventTypes.Contains(e.EventType))
            .ToList();

        if (relevantDomainEvents.Count == 0)
        {
            return;
        }

        foreach (var domainEvent in domainEvents)
        {
            var readModelIds = GetReadModelIds(domainEvent);

            foreach (var readModelId in readModelIds)
            {
                if (readModelCacheHelper.TryGetReadModelById(readModelId, out var readModel))
                {
                    var readModelContext = new ReadModelContext(serviceProvider, readModelId, false);
                    await readModelDomainEventApplier.UpdateReadModelAsync(readModel, [domainEvent], readModelContext,
                        cancellationToken);
                    if (readModelContext.IsMarkedForDeletion)
                    {
                        readModelCacheHelper.Remove(readModelId);
                    }
                }
            }
        }
    }

    protected abstract IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent);
}