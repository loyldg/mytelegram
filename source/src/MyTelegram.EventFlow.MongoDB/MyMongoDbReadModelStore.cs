//using System.Linq.Expressions;
//using EventFlow.Core;
//using EventFlow.Core.RetryStrategies;
//using EventFlow.Extensions;
//using EventFlow.MongoDB.ReadStores;
//using Microsoft.Extensions.Logging;
//using MongoDB.Driver;

//namespace MyTelegram.EventFlow.MongoDB;

//public class MyMongoDbReadModelStore<TReadModel, TMongoDbContext> : MyMongoDbReadModelStore<TReadModel>,
//    IMyMongoDbReadModelStore<TReadModel, TMongoDbContext>
//    where TReadModel : class, IMongoDbReadModel
//    where TMongoDbContext : IMongoDbContext
//{
//    public MyMongoDbReadModelStore(ILogger<MongoDbReadModelStore<TReadModel>> logger,
//        IMongoDbContextProvider<TMongoDbContext> mongoDbContextProvider,
//        IReadModelDescriptionProvider readModelDescriptionProvider,
//        ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> transientFaultHandler) : base(logger,
//        mongoDbContextProvider.CreateContext().GetDatabase(),
//        readModelDescriptionProvider,
//        transientFaultHandler)
//    {
//    }
//}

//public class MyMongoDbReadModelStore<TReadModel> : MongoDbReadModelStore<TReadModel>,
//    IMyMongoDbReadModelStore<TReadModel>
//    where TReadModel : class, IMongoDbReadModel
//{
//    private readonly ILogger<MongoDbReadModelStore<TReadModel>> _logger;
//    private readonly IMongoDatabase _mongoDatabase;
//    private readonly IReadModelDescriptionProvider _readModelDescriptionProvider;

//    public MyMongoDbReadModelStore(ILogger<MongoDbReadModelStore<TReadModel>> logger,
//        IMongoDatabase mongoDatabase,
//        IReadModelDescriptionProvider readModelDescriptionProvider,
//        ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> transientFaultHandler) : base(logger,
//        mongoDatabase,
//        readModelDescriptionProvider,
//        transientFaultHandler)
//    {
//        _logger = logger;
//        _mongoDatabase = mongoDatabase;
//        _readModelDescriptionProvider = readModelDescriptionProvider;
//    }

//    public Task<IAsyncCursor<TResult>> FindAsync<TResult>(Expression<Func<TReadModel, bool>> filter,
//        FindOptions<TReadModel, TResult>? options = null,
//        CancellationToken cancellationToken = default)
//    {
//        var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<TReadModel>();
//        var collection = _mongoDatabase.GetCollection<TReadModel>(readModelDescription.RootCollectionName.Value);

//        _logger.LogTrace(
//            "Finding read model '{ReadModel}' with expression '{Filter}' from collection '{RootCollectionName}'",
//            typeof(TReadModel).PrettyPrint(),
//            filter,
//            readModelDescription.RootCollectionName);

//        return collection.FindAsync(filter, options, cancellationToken);
//    }
//}
