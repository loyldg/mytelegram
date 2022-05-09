using MongoDB.Driver;

namespace EventFlow.MongoDB.ReadStores;

public interface IMongoDbContext
{
    IMongoDatabase Database { get; }
}
