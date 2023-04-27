namespace MyTelegram.EventFlow.MongoDB;

public class DefaultMongoDbContextProvider : IMongoDbContextProvider<DefaultMongoDbContext>
{
    private readonly DefaultMongoDbContext _defaultMongoDbContext;

    public DefaultMongoDbContextProvider(DefaultMongoDbContext defaultMongoDbContext)
    {
        _defaultMongoDbContext = defaultMongoDbContext;
    }

    public DefaultMongoDbContext CreateContext()
    {
        return _defaultMongoDbContext;
    }
}