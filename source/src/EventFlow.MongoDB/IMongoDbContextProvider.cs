using MongoDB.Driver;

namespace EventFlow.MongoDB;

public interface IMongoDbContextProvider<out TMongoDbContext> where TMongoDbContext : IMongoDatabase
{
    TMongoDbContext CreateContext();
}
