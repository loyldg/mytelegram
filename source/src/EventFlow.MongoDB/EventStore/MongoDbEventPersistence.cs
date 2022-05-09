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

using EventFlow.Aggregates;
using EventFlow.Core;
using EventFlow.EventStores;
using EventFlow.Exceptions;
using EventFlow.MongoDB.ValueObjects;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EventFlow.MongoDB.EventStore;

public class MyMongoDbEventPersistence : IEventPersistence
{
    public static readonly string CollectionName = "eventflow.events";
    private readonly ILogger<MyMongoDbEventPersistence> _logger;
    private readonly IMongoDatabase _mongoDatabase;

    public MyMongoDbEventPersistence(ILogger<MyMongoDbEventPersistence> log,
        IMongoDatabase mongoDatabase)
    {
        _logger = log;
        _mongoDatabase = mongoDatabase;
    }

    private IMongoCollection<MongoDbEventDataModel> MongoDbEventStoreCollection =>
        _mongoDatabase.GetCollection<MongoDbEventDataModel>(CollectionName);

    public async Task<AllCommittedEventsPage> LoadAllCommittedEvents(GlobalPosition globalPosition,
        int pageSize,
        CancellationToken cancellationToken)
    {
        if (globalPosition.IsStart)
        {
            var eventDataModels = await MongoDbEventStoreCollection
                .Find(FilterDefinition<MongoDbEventDataModel>.Empty)
                .Limit(pageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
            var nextPosition = globalPosition;
            if (eventDataModels.Any())
            {
                nextPosition = new GlobalPosition(eventDataModels.Last().Id.ToString());
            }

            return new AllCommittedEventsPage(nextPosition, eventDataModels);
        } else
        {
            var startPosition = new ObjectId(globalPosition.Value);
            var eventDataModels = await MongoDbEventStoreCollection.Find(p => p.Id > startPosition)
                .Limit(pageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
            var nextPosition = globalPosition;
            if (eventDataModels.Any())
            {
                nextPosition = new GlobalPosition(eventDataModels.Last().Id.ToString());
            }

            return new AllCommittedEventsPage(nextPosition, eventDataModels);
        }
    }

    public async Task<IReadOnlyCollection<ICommittedDomainEvent>> CommitEventsAsync(IIdentity id,
        IReadOnlyCollection<SerializedEvent> serializedEvents,
        CancellationToken cancellationToken)
    {
        if (!serializedEvents.Any())
        {
            return Array.Empty<ICommittedDomainEvent>();
        }

        var eventDataModels = serializedEvents
            .Select((e,
                _) => new MongoDbEventDataModel {
                AggregateId = id.Value,
                AggregateName = e.Metadata[MetadataKeys.AggregateName],
                BatchId = Guid.Parse(e.Metadata[MetadataKeys.BatchId]),
                Data = e.SerializedData,
                Metadata = e.SerializedMetadata,
                AggregateSequenceNumber = e.AggregateSequenceNumber
            })
            .OrderBy(x => x.AggregateSequenceNumber)
            .ToList();

        _logger.LogTrace("Committing {EventCount} events to MongoDb event store for entity with ID '{AggregateId}'",
            eventDataModels.Count,
            id);
        try
        {
            await MongoDbEventStoreCollection
                .InsertManyAsync(eventDataModels, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (MongoBulkWriteException e)
        {
            throw new OptimisticConcurrencyException(e.Message, e);
        }

        return eventDataModels;
    }

    public async Task<IReadOnlyCollection<ICommittedDomainEvent>> LoadCommittedEventsAsync(IIdentity id,
        int fromEventSequenceNumber,
        CancellationToken cancellationToken)
    {
        return await MongoDbEventStoreCollection
            .Find(model => model.AggregateId == id.Value && model.AggregateSequenceNumber >= fromEventSequenceNumber)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task DeleteEventsAsync(IIdentity id,
        CancellationToken cancellationToken)
    {
        var affectedRows = await MongoDbEventStoreCollection
            .DeleteManyAsync(x => x.AggregateId == id.Value, cancellationToken)
            .ConfigureAwait(false);

        _logger.LogTrace("Deleted entity with ID '{AggregateId}' by deleting all of its {EventCount} events",
            id,
            affectedRows.DeletedCount);
    }
}
