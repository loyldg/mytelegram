using EventFlow.MongoDB.EventStore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace MyTelegram.Messenger.Services.Impl;

public interface IMongoDbIdGenerator : IIdGenerator
{

}

public class MongoDbIdGenerator : IMongoDbIdGenerator
{
    private readonly IMongoDbEventSequenceStore _eventSequenceStore;

    public MongoDbIdGenerator(IMongoDbEventSequenceStore eventSequenceStore)
    {
        _eventSequenceStore = eventSequenceStore;
    }

    public async Task<int> NextIdAsync(IdType idType, long id, int step = 1, CancellationToken cancellationToken = default)
    {
        return (int)(await NextLongIdAsync(idType, id, step, cancellationToken));
    }

    public Task<long> NextLongIdAsync(IdType idType, long id = 0, int step = 1, CancellationToken cancellationToken = default)
    {
        var nextId = _eventSequenceStore.GetNextSequence($"{idType}-{id}");
        return Task.FromResult(nextId);
    }
}

public interface IRedisIdGenerator : IIdGenerator
{

}

public class RedisIdGenerator : IRedisIdGenerator
{
    private readonly IConfiguration _configuration;
    private ConnectionMultiplexer? _connection;
    private IDatabaseAsync? _database;

    public RedisIdGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<int> NextIdAsync(IdType idType, long id, int step = 1, CancellationToken cancellationToken = default)
    {
        return (int)(await NextLongIdAsync(idType, id, step, cancellationToken));
    }

    public async Task<long> NextLongIdAsync(IdType idType, long id = 0, int step = 1, CancellationToken cancellationToken = default)
    {
        var db = await GetDatabaseAsync();
        return await db.StringIncrementAsync(GetKey(idType, id));
    }

    private string GetKey(IdType idType, long id)
    {
        return MyCacheKey.With("seq", $"{idType}", $"{id}");
        //return $"m:seq:{idType}_{id}";
    }

    private async Task<IDatabaseAsync> GetDatabaseAsync()
    {
        if (_database == null)
        {
            var connection = await CreateConnectionAsync();
            _database = connection.GetDatabase();
        }

        return _database;
    }

    private async Task<ConnectionMultiplexer> CreateConnectionAsync()
    {
        if (_connection == null || !_connection.IsConnected)
        {
            var connectionString = _configuration.GetValue<string>("Redis:Configuration");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Redis configuration is null,section name is 'Redis:Configuration'");
            }

            _connection =
                await ConnectionMultiplexer.ConnectAsync(connectionString);
        }

        return _connection;
    }
}