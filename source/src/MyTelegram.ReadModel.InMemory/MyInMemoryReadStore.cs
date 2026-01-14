using MyTelegram.EventFlow.ReadStores;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace MyTelegram.ReadModel.InMemory;

public class MyInMemoryReadStore<TReadModel>(ILogger<MyInMemoryReadStore<TReadModel>> logger)
    : ReadModelStore<TReadModel>(logger), IMyInMemoryReadStore<TReadModel>
    where TReadModel : class, IReadModel
{
    private readonly ConcurrentDictionary<string, ReadModelEnvelope<TReadModel>> _readModels = new();

    public override Task<ReadModelEnvelope<TReadModel>> GetAsync(string id, CancellationToken cancellationToken)
    {
        if (_readModels.TryGetValue(id, out var readModelEnvelope))
        {
            return Task.FromResult(readModelEnvelope);
        }

        return Task.FromResult(ReadModelEnvelope<TReadModel>.Empty(id));
    }

    public override async Task UpdateAsync(IReadOnlyCollection<ReadModelUpdate> readModelUpdates, IReadModelContextFactory readModelContextFactory,
        Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken, Task<ReadModelUpdateResult<TReadModel>>> updateReadModel, CancellationToken cancellationToken)
    {
        // Console.WriteLine($"MyInMemoryReadStore<{typeof(TReadModel).Name}> hash code:{this.GetHashCode()} count={_readModels.Count}");
        foreach (var readModelUpdate in readModelUpdates)
        {
            var readModelId = readModelUpdate.ReadModelId;

            //var isNew = !_readModels.TryGetValue(readModelId, out var readModelEnvelope);
            var isNew = !_readModels.TryGetValue(readModelId, out var readModelEnvelope);

            //if (isNew)
            //{
            //    continue;
            //}

            if (isNew || readModelEnvelope == null)
            {
                readModelEnvelope = ReadModelEnvelope<TReadModel>.Empty(readModelId);
            }

            var readModelContext = readModelContextFactory.Create(readModelId, isNew);

            var readModelUpdateResult = await updateReadModel(
                    readModelContext,
                    readModelUpdate.DomainEvents,
                    readModelEnvelope,
                    cancellationToken)
                .ConfigureAwait(false);
            if (!readModelUpdateResult.IsModified)
            {
                continue;
            }
            var oldReadModelEnvelope = readModelEnvelope;
            readModelEnvelope = readModelUpdateResult.Envelope;

            if (readModelContext.IsMarkedForDeletion)
            {
                _readModels.TryRemove(readModelId, out _);
            }
            else
            {
                //_readModels.AddOrUpdate(readModelId, readModelEnvelope, (rid, oldData) => readModelEnvelope);
                //_readModels.TryUpdate(readModelId, readModelEnvelope, null);
                //if (_readModels.ContainsKey(readModelId))
                //{
                //    _readModels.TryRemove(readModelId,out _);
                //}

                //_readModels.TryAdd(readModelId, readModelEnvelope);
                if (isNew)
                {
                    _readModels.TryAdd(readModelId, readModelEnvelope);
                }
                else
                {
                    if (!_readModels.TryUpdate(readModelId, readModelEnvelope, oldReadModelEnvelope))
                    {
                        Console.WriteLine("Update inmemory readmodel failed");
                    }
                }

                // Console.WriteLine($"Update in-memory readmodel({typeof(TReadModel)}) {readModelUpdates.Count} ({readModelUpdate.DomainEvents.FirstOrDefault()?.GetAggregateEvent().GetType().Name}):{readModelEnvelope.ReadModelId} total:{_readModels.Count} version:{oldVersion}->{readModelEnvelope.Version}");
            }
        }
    }

    public Task<IReadOnlyCollection<TReadModel>> FindAsync(Predicate<TReadModel> predicate, CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyCollection<TReadModel>>([.. _readModels.Values
            .Where(p => predicate(p.ReadModel))
            .Select(p => p.ReadModel)
            .ToList()]);
    }

    public Task<IQueryable<TReadModel>> AsQueryable(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_readModels.Values.Select(p => p.ReadModel).AsQueryable());
    }

    public void Add(string id, TReadModel readModel, long? version)
    {
        _readModels.TryAdd(id, ReadModelEnvelope<TReadModel>.With(id, readModel, version));
    }

    public override Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        _readModels.TryRemove(id, out _);

        return Task.CompletedTask;
    }

    public override Task DeleteAllAsync(CancellationToken cancellationToken)
    {
        _readModels.Clear();

        return Task.CompletedTask;
    }

    #region IQueryOnlyReadModelStore

    public IQueryable<TReadModel> GetAll()
    {
        return _readModels.Values.Select(p => p.ReadModel).AsQueryable();
    }

    public Task<IReadOnlyCollection<TReadModel>> FindAsync(Expression<Func<TReadModel, bool>> filter, int skip = 0, int limit = 0, SortOptions<TReadModel>? sort = null,
        CancellationToken cancellationToken = default)
    {
        return FindAsync(filter, p => p, skip, limit, sort, cancellationToken);
    }

    public Task<IReadOnlyCollection<TResult>> FindAsync<TResult>(Expression<Func<TReadModel, bool>> filter, Expression<Func<TReadModel, TResult>> createResult, int skip = 0, int limit = 0,
        SortOptions<TReadModel>? sort = null, CancellationToken cancellationToken = default)
    {
        var query = _readModels.Values.Select(p => p.ReadModel).AsQueryable().Where(filter);
        var query2 = CreateQuery(query, createResult, skip, limit, sort);

        return Task.FromResult<IReadOnlyCollection<TResult>>([.. query2.ToList()]);
    }

    public Task<TReadModel?> FirstOrDefaultAsync(Expression<Func<TReadModel, bool>> filter, CancellationToken cancellationToken = default)
    {
        return FirstOrDefaultAsync(filter, p => p, cancellationToken: cancellationToken);
    }

    public Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<TReadModel, bool>> filter, Expression<Func<TReadModel, TResult>> createResult, SortOptions<TReadModel>? sort = null,
        CancellationToken cancellationToken = default)
    {
        var query = _readModels.Values.Select(p => p.ReadModel).AsQueryable().Where(filter);
        var query2 = CreateQuery(query, createResult, 0, 0, sort);

        return Task.FromResult(query2.FirstOrDefault());
    }

    public Task<long> CountAsync(Expression<Func<TReadModel, bool>> filter, CancellationToken cancellationToken = default)
    {
        var query = _readModels.Values.Select(p => p.ReadModel).AsQueryable();
        var query2 = CreateQuery(query, p => p);

        return Task.FromResult<long>(query2.Count());
    }

    public async Task<List<TResult>> GroupByAsync<TKey, TResult>(Expression<Func<TReadModel, bool>>? filter, 
        Expression<Func<TReadModel, TKey>> keySelector, 
        Expression<Func<IGrouping<TKey, TReadModel>, TResult>> resultSelector)
    {
        throw new NotImplementedException();
    }

    private static IQueryable<T2> CreateQuery<T1, T2>(IQueryable<T1> query, Expression<Func<T1, T2>> createResult, int skip = 0, int limit = 0, SortOptions<T1>? sort = null)
    {
        if (sort != null)
        {
            switch (sort.SortType)
            {
                case SortType.None:
                    break;
                case SortType.Ascending:
                    query = query.OrderBy(sort.Sort);
                    break;
                case SortType.Descending:
                    query = query.OrderByDescending(sort.Sort);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sort));
            }
        }

        if (skip > 0)
        {
            query = query.Skip(skip);
        }

        if (limit > 0)
        {
            query = query.Take(limit);
        }

        return query.Select(createResult);
    }
    #endregion
}