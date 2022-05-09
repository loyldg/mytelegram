// The MIT License (MIT)
// 
// Copyright (c) 2015-2021 Rasmus Mikkelsen
// Copyright (c) 2015-2021 eBay Software Foundation
// https://github.com/eventflow/EventFlow
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Linq.Expressions;
using EventFlow.Aggregates;
using EventFlow.Core;
using EventFlow.Core.RetryStrategies;
using EventFlow.Exceptions;
using EventFlow.Extensions;
using EventFlow.MongoDB.ValueObjects;
using EventFlow.ReadStores;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace EventFlow.MongoDB.ReadStores;

public class MyMongoDbReadModelStore<TReadModel, TMongoDbContext> :
    MongoDbReadModelStore<TReadModel>,
    IMyMongoDbReadModelStore<TReadModel>,
    IMyMongoDbReadModelStore<TReadModel, TMongoDbContext>
    where TReadModel : class, IMongoDbReadModel
    where TMongoDbContext : class, IMongoDbContext
{
    private readonly IMongoDbContextProvider<TMongoDbContext> _contextProvider;

    private readonly ILogger<MyMongoDbReadModelStore<TReadModel>> _logger;

    private readonly IReadModelDescriptionProvider _readModelDescriptionProvider;

    public MyMongoDbReadModelStore(ILogger<MyMongoDbReadModelStore<TReadModel>> log,
        IReadModelDescriptionProvider readModelDescriptionProvider,
        ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> transientFaultHandler,
        IMongoDbContextProvider<TMongoDbContext> contextProvider) :
        base(log, contextProvider.CreateMongoDbContext().Database, readModelDescriptionProvider, transientFaultHandler)
    {
        _logger = log;
        _readModelDescriptionProvider = readModelDescriptionProvider;
        _contextProvider = contextProvider;
    }

    public async Task<IAsyncCursor<TResult>> FindAsync<TResult>(Expression<Func<TReadModel, bool>> filter,
        FindOptions<TReadModel, TResult>? options = null,
        CancellationToken cancellationToken = default)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = _contextProvider.CreateMongoDbContext().Database
            .GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

        _logger.LogTrace(
            "Finding read model '{ReadModel}' with expression '{Filter}' from collection '{RootCollectionName}'", typeof(TReadModel).PrettyPrint(), filter, readModelDescription.RootCollectionName);

        return await collection.FindAsync(filter, options, cancellationToken).ConfigureAwait(false);
    }
}

public class MyMongoDbReadModelStore<TReadModel> :
    MongoDbReadModelStore<TReadModel>,
    IMyMongoDbReadModelStore<TReadModel>
    where TReadModel : class, IMongoDbReadModel
{
    private readonly ILogger<MyMongoDbReadModelStore<TReadModel>> _log;
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IReadModelDescriptionProvider _readModelDescriptionProvider;

    public MyMongoDbReadModelStore(ILogger<MyMongoDbReadModelStore<TReadModel>> log,
        IMongoDatabase mongoDatabase,
        IReadModelDescriptionProvider readModelDescriptionProvider,
        ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> transientFaultHandler) : base(log,
        mongoDatabase,
        readModelDescriptionProvider,
        transientFaultHandler)
    {
        _log = log;
        _mongoDatabase = mongoDatabase;
        _readModelDescriptionProvider = readModelDescriptionProvider;
    }

    public async Task<IAsyncCursor<TResult>> FindAsync<TResult>(Expression<Func<TReadModel, bool>> filter,
        FindOptions<TReadModel, TResult>? options = null,
        CancellationToken cancellationToken = default)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = _mongoDatabase.GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

        _log.LogTrace(
            "Finding read model '{ReadModel}' with expression '{Filter}' from collection '{RootCollectionName}'",
            typeof(TReadModel).PrettyPrint(),
            filter,
            readModelDescription.RootCollectionName);

        return await collection.FindAsync(filter, options, cancellationToken).ConfigureAwait(false);
    }
}

public class MongoDbReadModelStore<TReadModel> : IMongoDbReadModelStore<TReadModel>
    where TReadModel : class, IMongoDbReadModel
{
    private readonly ILogger<MongoDbReadModelStore<TReadModel>> _logger;
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IReadModelDescriptionProvider _readModelDescriptionProvider;
    private readonly ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> _transientFaultHandler;

    public MongoDbReadModelStore(
        ILogger<MongoDbReadModelStore<TReadModel>> log,
        IMongoDatabase mongoDatabase,
        IReadModelDescriptionProvider readModelDescriptionProvider,
        ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> transientFaultHandler)
    {
        _logger = log;
        _mongoDatabase = mongoDatabase;
        _readModelDescriptionProvider = readModelDescriptionProvider;
        _transientFaultHandler = transientFaultHandler;
    }

    public async Task DeleteAsync(string id,
        CancellationToken cancellationToken)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();

        _logger.LogInformation(
            "Deleting '{ReadModelType}' with id '{Id}', from '{@RootCollectionName}'!",
            typeof(TReadModel).PrettyPrint(),
            id,
            readModelDescription.RootCollectionName);

        var collection = _mongoDatabase.GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);
        await collection.DeleteOneAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAllAsync(CancellationToken cancellationToken)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();

        _logger.LogInformation(
            "Deleting ALL '{ReadModelType}' by DROPPING COLLECTION '{@RootCollectionName}'!",
            typeof(TReadModel).PrettyPrint(),
            readModelDescription.RootCollectionName);

        await _mongoDatabase.DropCollectionAsync(readModelDescription.RootCollectionName.Value, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ReadModelEnvelope<TReadModel>> GetAsync(string id,
        CancellationToken cancellationToken)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();

        //_logger.LogTrace($"Fetching read model '{typeof(TReadModel).PrettyPrint()}' with _id '{id}' from collection '{readModelDescription.RootCollectionName}'");
        _logger.LogTrace(
            "Fetching read model '{ReadModelType}' with _id '{Id}' from collection '{@RootCollectionName}'",
            typeof(TReadModel).PrettyPrint(),
            id,
            readModelDescription.RootCollectionName);

        var collection = _mongoDatabase.GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);
        var filter = Builders<TReadModel>.Filter.Eq(readModel => readModel.Id, id);
        var result = await collection.Find(filter).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        if (result == null)
        {
            return ReadModelEnvelope<TReadModel>.Empty(id);
        }

        return ReadModelEnvelope<TReadModel>.With(id, result);
    }


    public async Task<IAsyncCursor<TReadModel>> FindAsync(Expression<Func<TReadModel, bool>> filter,
        FindOptions<TReadModel, TReadModel>? options = null,
        CancellationToken cancellationToken = new())
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = _mongoDatabase.GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

        _logger.LogTrace(
            "Finding read model '{ReadModelType}' with expression '{Filter}' from collection '{RootCollectionName}'",
            typeof(TReadModel).PrettyPrint(),
            filter.ToString(),
            readModelDescription.RootCollectionName.ToString());

        return await collection.FindAsync(filter, options, cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateAsync(IReadOnlyCollection<ReadModelUpdate> readModelUpdates,
        IReadModelContextFactory readModelContextFactory,
        Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken,
            Task<ReadModelUpdateResult<TReadModel>>> updateReadModel,
        CancellationToken cancellationToken)
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var readModelIds = readModelUpdates
            .Select(u => u.ReadModelId)
            .Distinct()
            .OrderBy(i => i)
            .ToList();

        _logger.LogTrace(
            "Updating read models of type '{TypeName}' with _ids '{Ids}' in collection '{RootCollectionName}'",
            typeof(TReadModel).PrettyPrint(),
            string.Join(", ", readModelIds),
            readModelDescription.RootCollectionName);

        foreach (var readModelUpdate in readModelUpdates)
        {
            await _transientFaultHandler.TryAsync(
                    c => UpdateReadModelAsync(readModelDescription,
                        readModelUpdate,
                        readModelContextFactory,
                        updateReadModel,
                        c),
                    Label.Named("mongodb-read-model-update"),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }

    public IQueryable<TReadModel> AsQueryable()
    {
        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
        var collection = _mongoDatabase.GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);
        return collection.AsQueryable();
    }

    private async Task UpdateReadModelAsync(ReadModelDescription readModelDescription,
        ReadModelUpdate readModelUpdate,
        IReadModelContextFactory readModelContextFactory,
        Func<IReadModelContext, IReadOnlyCollection<IDomainEvent>, ReadModelEnvelope<TReadModel>, CancellationToken,
            Task<ReadModelUpdateResult<TReadModel>>> updateReadModel,
        CancellationToken cancellationToken)
    {
        var collection = _mongoDatabase.GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);
        var filter = Builders<TReadModel>.Filter.Eq(readModel => readModel.Id, readModelUpdate.ReadModelId);
        var result = await collection.Find(filter).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        var isNew = result == null;

        var readModelEnvelope = !isNew
            ? ReadModelEnvelope<TReadModel>.With(readModelUpdate.ReadModelId, result!)
            : ReadModelEnvelope<TReadModel>.Empty(readModelUpdate.ReadModelId);

        var readModelContext = readModelContextFactory.Create(readModelUpdate.ReadModelId, isNew);
        var readModelUpdateResult =
            await updateReadModel(readModelContext,
                readModelUpdate.DomainEvents,
                readModelEnvelope,
                cancellationToken).ConfigureAwait(false);

        if (!readModelUpdateResult.IsModified)
        {
            return;
        }

        if (readModelContext.IsMarkedForDeletion)
        {
            await DeleteAsync(readModelUpdate.ReadModelId, cancellationToken).ConfigureAwait(false);
            return;
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
                cancellationToken).ConfigureAwait(false);
        }
        catch (MongoWriteException e)
        {
            throw new OptimisticConcurrencyException(
                $"Read model '{readModelUpdate.ReadModelId}' updated by another",
                e);
        }
    }
}
