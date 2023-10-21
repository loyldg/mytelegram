using MongoDB.Driver;

namespace MyTelegram.EventFlow.MongoDB;

public interface IMongoDbContext
{
    IMongoDatabase GetDatabase();
}