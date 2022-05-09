namespace EventFlow.MongoDB.ReadStores;

public interface IMongoDbContextProvider<out TMongoDbContext> where TMongoDbContext : IMongoDbContext
{
    TMongoDbContext CreateMongoDbContext();
}
