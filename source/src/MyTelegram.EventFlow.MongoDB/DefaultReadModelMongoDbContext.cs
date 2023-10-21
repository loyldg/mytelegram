using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MyTelegram.EventFlow.MongoDB;

public class DefaultReadModelMongoDbContext : IMongoDbContext
{
    private readonly IConfiguration _configuration;
    private IMongoDatabase? _database;

    public DefaultReadModelMongoDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IMongoDatabase GetDatabase()
    {
        if (_database == null)
        {
            var connectionString = _configuration.GetConnectionString(GetConnectionStringName());
            var databaseName = _configuration.GetValue<string>(GetKeyOfDatabaseNameInConfiguration());
            var client = new MongoClient(connectionString);
            //client.Settings.MaxConnecting = 5000;
            //client.Settings.MaxConnectionPoolSize = 5000;
            _database = client.GetDatabase(databaseName);
        }

        return _database;
    }

    protected virtual string GetConnectionStringName() => "Default";
    protected virtual string GetKeyOfDatabaseNameInConfiguration() => "App:DatabaseName";
}