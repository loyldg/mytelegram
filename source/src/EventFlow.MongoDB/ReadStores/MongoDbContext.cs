using MongoDB.Driver;

namespace EventFlow.MongoDB.ReadStores;

public class MongoDbContext : IMongoDbContext
{
    public MongoDbContext(IMongoDatabase database)
    {
        Database = database;
    }

    public IMongoDatabase Database { get; }
}
