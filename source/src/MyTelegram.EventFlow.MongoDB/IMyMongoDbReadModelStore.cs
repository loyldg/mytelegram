using System.Linq.Expressions;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using MongoDB.Driver;

namespace MyTelegram.EventFlow.MongoDB;

public interface IMyMongoDbReadModelStore<TReadModel> : IMongoDbReadModelStore<TReadModel>
    where TReadModel : class, IReadModel
{
    Task<IAsyncCursor<TResult>> FindAsync<TResult>(
        Expression<Func<TReadModel, bool>> filter,
        FindOptions<TReadModel, TResult>? options = null,
        CancellationToken cancellationToken = default);
}

public interface IMyMongoDbReadModelStore<TReadModel, TMongoDbContext> : IMyMongoDbReadModelStore<TReadModel> where TReadModel : class, IReadModel
{
}