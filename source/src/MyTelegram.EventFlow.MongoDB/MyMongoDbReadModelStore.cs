using EventFlow.Aggregates;
using EventFlow.Core;
using EventFlow.Core.RetryStrategies;
using EventFlow.Exceptions;
using EventFlow.Extensions;
using EventFlow.MongoDB.ReadStores;
using EventFlow.MongoDB.ValueObjects;
using EventFlow.ReadStores;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Linq.Expressions;
using MyTelegram.EventFlow.ReadStores;

namespace MyTelegram.EventFlow.MongoDB;

public class MyMongoDbReadModelStore<TReadModel>(
    ILogger<MyTelegram.EventFlow.MongoDB.ReadStores.MongoDbReadModelStore<TReadModel>> logger,
    IReadModelDescriptionProvider readModelDescriptionProvider,
    IEnumerable<IReadModelWriteInterceptor> readModelWriteInterceptors,
    ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> transientFaultHandler,
    IMongoDbContext mongoDbContext)
    :
        MyMongoDbReadModelStore<TReadModel, IMongoDbContext>(logger, readModelDescriptionProvider,
            readModelWriteInterceptors,
            transientFaultHandler, mongoDbContext)
    where TReadModel : class, IMongoDbReadModel;

public class MyMongoDbReadModelStore<TReadModel, TDbContext>(
    ILogger<MyTelegram.EventFlow.MongoDB.ReadStores.MongoDbReadModelStore<TReadModel>> logger,
    IReadModelDescriptionProvider readModelDescriptionProvider,
    IEnumerable<IReadModelWriteInterceptor> readModelWriteInterceptors,
    ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> transientFaultHandler,
    TDbContext dbContext)
    : MyTelegram.EventFlow.MongoDB.ReadStores.MongoDbReadModelStore<TReadModel>(logger, dbContext.GetDatabase(),
        readModelDescriptionProvider, transientFaultHandler), IMyMongoDbReadModelStore<TReadModel>
    where TReadModel : class, IMongoDbReadModel
    where TDbContext : IMongoDbContext
{
    private readonly ILogger<MyTelegram.EventFlow.MongoDB.ReadStores.MongoDbReadModelStore<TReadModel>> _logger = logger;
    private readonly IReadModelDescriptionProvider _readModelDescriptionProvider = readModelDescriptionProvider;

    private IMongoDatabase GetDatabase() => dbContext.GetDatabase();

    public Task<IAggregateFluent<TResult>> AggregateAsync<TResult, TKey>(
        Expression<Func<TReadModel, bool>> filter,
        Expression<Func<TReadModel, TKey>> id,
        Expression<Func<IGrouping<TKey, TReadModel>, TResult>> group,
        AggregateOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = GetDatabase().GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

        return Task.FromResult(collection.Aggregate()
                .Match(filter)
                .Group(id, group))
            ;
    }

    protected override async Task UpdateReadModelAsync(ReadModelDescription readModelDescription, ReadModelUpdate readModelUpdate,
        IReadModelContextFactory readModelContextFactory, Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken, Task<ReadModelUpdateResult<TReadModel>>> updateReadModel, CancellationToken cancellationToken)
    {
        var collection = GetDatabase().GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);
        var filter = Builders<TReadModel>.Filter.Eq(readModel => readModel.Id, readModelUpdate.ReadModelId);
        var result = collection.Find(filter).FirstOrDefault();

        var isNew = result == null;

        var readModelEnvelope = !isNew
            ? ReadModelEnvelope<TReadModel>.With(readModelUpdate.ReadModelId, result)
            : ReadModelEnvelope<TReadModel>.Empty(readModelUpdate.ReadModelId);

        var readModelContext = readModelContextFactory.Create(readModelUpdate.ReadModelId, isNew);
        var readModelUpdateResult =
            await updateReadModel(readModelContext, readModelUpdate.DomainEvents, readModelEnvelope,
                cancellationToken).ConfigureAwait(false);

        if (!readModelUpdateResult.IsModified)
        {
            return;
        }

        foreach (var readModelWriteInterceptor in readModelWriteInterceptors)
        {
            readModelWriteInterceptor.OnUpdate(readModelContext, readModelUpdateResult.Envelope.ReadModel, readModelUpdate.DomainEvents);
        }

        if (readModelContext.IsMarkedForDeletion)
        {
            if (readModelEnvelope.ReadModel is ISoftDelete softDelete)
            {
                softDelete.IsDeleted = true;
            }
            else
            {
                await DeleteAsync(readModelUpdate.ReadModelId, cancellationToken);
                return;
            }
        }

        readModelEnvelope = readModelUpdateResult.Envelope;
        var originalVersion = readModelEnvelope.ReadModel.Version;
        readModelEnvelope.ReadModel.Version = readModelEnvelope.Version;
        try
        {
            await collection.ReplaceOneAsync(
                x => x.Id == readModelUpdate.ReadModelId && x.Version == originalVersion,
                readModelEnvelope.ReadModel,
                new ReplaceOptions { IsUpsert = true },
                cancellationToken);
        }
        catch (MongoWriteException e)
        {
            throw new OptimisticConcurrencyException(
                $"Read model '{readModelUpdate.ReadModelId}' updated by another",
                e);
        }
    }

    public async Task<IAsyncCursor<TResult>> FindAsync<TResult>(Expression<Func<TReadModel, bool>> filter, FindOptions<TReadModel, TResult>? options = null, CancellationToken cancellationToken = default)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = GetDatabase().GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

        _logger.LogTrace(
            "Finding read model '{ReadModel}' with expression '{Filter}' from collection '{RootCollectionName}'",
            typeof(TReadModel).PrettyPrint(),
            filter,
            readModelDescription.RootCollectionName);

        return await collection.FindAsync(filter, options, cancellationToken);
    }

    public Task<long> CountAsync(Expression<Func<TReadModel, bool>>? filter = null, CancellationToken cancellationToken = default)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = GetDatabase().GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

        return collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
    }
}
