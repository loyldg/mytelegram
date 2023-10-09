using MyTelegram.Messenger.Services.IdGenerator;
using MyTelegram.MessengerServer;

namespace MyTelegram.Messenger.Services.Impl;


public class MongoDbHighValueGenerator : IHiLoHighValueGenerator
{
    private readonly IMongoDbIdGenerator _idGenerator;

    public MongoDbHighValueGenerator(IMongoDbIdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    public Task<long> GetNewHighValueAsync(IdType idType, long key, CancellationToken cancellationToken = default)
    {
        return _idGenerator.NextLongIdAsync(idType, key, cancellationToken: cancellationToken);
    }
}

public class RedisHighValueGenerator : IHiLoHighValueGenerator
{
    private readonly IRedisIdGenerator _redisIdGenerator;

    public RedisHighValueGenerator(IRedisIdGenerator redisIdGenerator)
    {
        _redisIdGenerator = redisIdGenerator;
    }

    public Task<long> GetNewHighValueAsync(IdType idType, long key, CancellationToken cancellationToken = default)
    {
        return _redisIdGenerator.NextLongIdAsync(idType, key, cancellationToken: cancellationToken);
    }
}

public class HiLoHighValueGenerator : IHiLoHighValueGenerator
{
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;
    public HiLoHighValueGenerator(IOptions<MyTelegramMessengerServerOptions> options)
    {
        _options = options;
    }

    public async Task<long> GetNewHighValueAsync(IdType idType,
        long key,
        CancellationToken cancellationToken = default)
    {
        if ((byte)idType > 25)
        {
            Console.WriteLine($"############ new high value error:{idType}");
        }

        try
        {
            var client = GrpcClientFactory.CreateIdGeneratorServiceClient(_options.Value.IdGeneratorGrpcServiceUrl);
            var r = await client
                .GenerateNextHighValueAsync(new GenerateNextHighValueRequest { IdType = (int)idType, IdKey = key },
                    cancellationToken: cancellationToken)
                .ResponseAsync;
            return r.HighValue;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Get next id failed:{ex}");
            var client = GrpcClientFactory.CreateIdGeneratorServiceClient(_options.Value.IdGeneratorGrpcServiceUrl, true);
            var r = await client
                .GenerateNextHighValueAsync(new GenerateNextHighValueRequest { IdType = (int)idType, IdKey = key },
                    cancellationToken: cancellationToken)
                .ResponseAsync;
            return r.HighValue;
        }
    }
}
