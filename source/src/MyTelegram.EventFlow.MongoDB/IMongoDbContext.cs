using MongoDB.Driver;

namespace MyTelegram.EventFlow.MongoDB;

public interface IMongoDbContext
{
    //IMongoDatabase Database { get; }
    IMongoDatabase GetDatabase();
}