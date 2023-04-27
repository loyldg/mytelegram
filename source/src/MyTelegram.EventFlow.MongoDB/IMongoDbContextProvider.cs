namespace MyTelegram.EventFlow.MongoDB;

public interface IMongoDbContextProvider<out TMongoDbContext> where TMongoDbContext : IMongoDbContext// IMongoDatabase
{
    TMongoDbContext CreateContext();
}