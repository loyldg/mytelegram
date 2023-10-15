//namespace MyTelegram.EventFlow.MongoDB;

//public interface IMongoDbContextProvider<out TMongoDbContext> where TMongoDbContext : IMongoDbContext // IMongoDatabase
//{
//    TMongoDbContext CreateContext();
//}


namespace MyTelegram.EventFlow.MongoDB;

public interface IMongoDbContextFactory<out TMongoDbContext> where TMongoDbContext : IMongoDbContext
{
    TMongoDbContext CreateContext();
}

public class DefaultMongoDbContextFactory<TMongoDbContext> : IMongoDbContextFactory<TMongoDbContext> where TMongoDbContext : IMongoDbContext
{
    private readonly TMongoDbContext _dbContext;

    public DefaultMongoDbContextFactory(TMongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public TMongoDbContext CreateContext() => _dbContext;
}