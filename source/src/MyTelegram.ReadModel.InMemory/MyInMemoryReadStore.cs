namespace MyTelegram.ReadModel.InMemory;

public class MyInMemoryReadStore<TReadModel> : ReadModelStore<TReadModel>, IMyInMemoryReadStore<TReadModel>
        where TReadModel : class, IReadModel
{
    private readonly Dictionary<string, ReadModelEnvelope<TReadModel>> _readModels = new();
    private readonly AsyncLock _asyncLock = new();

    public MyInMemoryReadStore(
        ILogger<InMemoryReadStore<TReadModel>> logger)
        : base(logger)
    {
    }

    public async Task<IQueryable<TReadModel>> AsQueryable(CancellationToken cancellationToken = default)
    {
        using var r = await _asyncLock.WaitAsync(cancellationToken);
        return _readModels.Values.Select(p => p.ReadModel).AsQueryable();
    }

    public override async Task<ReadModelEnvelope<TReadModel>> GetAsync(
        string id,
        CancellationToken cancellationToken)
    {
        using (await _asyncLock.WaitAsync(cancellationToken).ConfigureAwait(false))
        {
            return _readModels.TryGetValue(id, out var readModelEnvelope)
                ? readModelEnvelope
                : ReadModelEnvelope<TReadModel>.Empty(id);
        }
    }

    public async Task<IReadOnlyCollection<TReadModel>> FindAsync(
        Predicate<TReadModel> predicate,
        CancellationToken cancellationToken)
    {
        using (await _asyncLock.WaitAsync(cancellationToken).ConfigureAwait(false))
        {
            return _readModels.Values
                .Where(e => predicate(e.ReadModel))
                .Select(e => e.ReadModel)
                .ToList();
        }
    }

    public override async Task DeleteAsync(
        string id,
        CancellationToken cancellationToken)
    {
        using (await _asyncLock.WaitAsync(cancellationToken).ConfigureAwait(false))
        {
            _readModels.Remove(id);
        }
    }

    public override async Task DeleteAllAsync(
        CancellationToken cancellationToken)
    {
        using (await _asyncLock.WaitAsync(cancellationToken).ConfigureAwait(false))
        {
            _readModels.Clear();
        }
    }

    public override async Task UpdateAsync(IReadOnlyCollection<ReadModelUpdate> readModelUpdates,
        IReadModelContextFactory readModelContextFactory,
        Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken, Task<ReadModelUpdateResult<TReadModel>>> updateReadModel,
        CancellationToken cancellationToken)
    {
        using (await _asyncLock.WaitAsync(cancellationToken).ConfigureAwait(false))
        {
            foreach (var readModelUpdate in readModelUpdates)
            {
                var readModelId = readModelUpdate.ReadModelId;

                var isNew = !_readModels.TryGetValue(readModelId, out var readModelEnvelope);

                if (isNew)
                {
                    readModelEnvelope = ReadModelEnvelope<TReadModel>.Empty(readModelId);
                }

                var readModelContext = readModelContextFactory.Create(readModelId, isNew);

                var readModelUpdateResult = await updateReadModel(
                    readModelContext,
                    readModelUpdate.DomainEvents,
                    readModelEnvelope!,
                    cancellationToken)
                    ;
                if (!readModelUpdateResult.IsModified)
                {
                    continue;
                }

                readModelEnvelope = readModelUpdateResult.Envelope;

                if (readModelContext.IsMarkedForDeletion)
                {
                    _readModels.Remove(readModelId);
                } else
                {
                    _readModels[readModelId] = readModelEnvelope;
                }
            }
        }
    }
}