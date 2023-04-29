using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MyTelegram.EventFlow.MongoDB;

public class DefaultMongoDbContext : IMongoDbContext
{
    private readonly IConfiguration _configuration;
    private IMongoDatabase? _database;

    public DefaultMongoDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IMongoDatabase GetDatabase()
    {
        if (_database == null)
        {
            var connectionString = _configuration.GetConnectionString("Default");
            var databaseName = _configuration.GetValue<string>("App:DatabaseName");
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        return _database;
    }
}
