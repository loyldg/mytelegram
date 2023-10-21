using MongoDB.Driver;

namespace MyTelegram.EventFlow.MongoDB;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _mongoDatabase;

    public MongoDbContext(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }

    public IMongoDatabase GetDatabase() => _mongoDatabase;
}