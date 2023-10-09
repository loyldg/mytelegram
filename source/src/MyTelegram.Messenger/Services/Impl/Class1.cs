using MyTelegram.EventFlow.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventFlow.Core.RetryStrategies;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using Microsoft.Extensions.Caching.Memory;
using MyTelegram.ReadModel.InMemory;

namespace MyTelegram.Messenger.Services.Impl;

//public class MyMongoDbReadModelStore2<TReadModel> :
//    MyMongoDbReadModelStore2<TReadModel, IMongoDbContext>
//    where TReadModel : class, IMongoDbReadModel
//{
//    public MyMongoDbReadModelStore2(ILogger<MongoDbReadModelStore<TReadModel>> logger, IReadModelDescriptionProvider readModelDescriptionProvider, ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> transientFaultHandler, IMongoDbContextFactory<IMongoDbContext> dbContextFactory, IMemoryCache memoryCache, IReadModelCacheStrategy readModelCacheStrategy, IReadModelUpdateStrategy readModelUpdateStrategy, IMyInMemoryReadStore<TReadModel> readStore, IReadModelCacheStrategy cacheStrategy) : base(logger, readModelDescriptionProvider, transientFaultHandler, dbContextFactory, memoryCache, readModelCacheStrategy, readModelUpdateStrategy, readStore, cacheStrategy)
//    {
//    }
//}

//public class MyMongoDbReadModelStore2<TReadModel, TDbContext> : MyMongoDbReadModelStore<TReadModel, TDbContext> where TDbContext : IMongoDbContext where TReadModel : class, IMongoDbReadModel
//{
//    private readonly IMyInMemoryReadStore<TReadModel> _readStore;
//    private readonly IReadModelCacheStrategy _cacheStrategy;
//    public MyMongoDbReadModelStore2(ILogger<MongoDbReadModelStore<TReadModel>> logger, IReadModelDescriptionProvider readModelDescriptionProvider, ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> transientFaultHandler, IMongoDbContextFactory<TDbContext> dbContextFactory, IMemoryCache memoryCache, IReadModelCacheStrategy readModelCacheStrategy, IReadModelUpdateStrategy readModelUpdateStrategy, IMyInMemoryReadStore<TReadModel> readStore, IReadModelCacheStrategy cacheStrategy) : base(logger, readModelDescriptionProvider, transientFaultHandler, dbContextFactory, memoryCache, readModelCacheStrategy, readModelUpdateStrategy)
//    {
//        _readStore = readStore;
//        _cacheStrategy = cacheStrategy;
//    }

//    public override async Task<ReadModelEnvelope<TReadModel>> GetAsync(string id, CancellationToken cancellationToken)
//    {
//        if (await _cacheStrategy.ShouldCacheReadModelAsync<TReadModel>())
//        {
//            var readModelEnvelope = await _readStore.GetAsync(id, cancellationToken);
//            if (readModelEnvelope.ReadModel == null)
//            {
//                readModelEnvelope = await base.GetAsync(id, cancellationToken);
//                _readStore.Add(id, readModelEnvelope.ReadModel, readModelEnvelope.Version);
//            }

//            Console.WriteLine($"Get readmodel from cache:{id}");
//        }

//        return await base.GetAsync(id, cancellationToken);
//    }
//}
