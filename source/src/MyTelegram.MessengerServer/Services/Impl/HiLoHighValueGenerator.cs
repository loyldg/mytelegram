using MyTelegram.GrpcService;
using MyTelegram.MessengerServer.Services.IdGenerator;

namespace MyTelegram.MessengerServer.Services.Impl;

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
        const byte maxIdType = 20;
        if ((byte)idType > maxIdType)
        {
            Console.WriteLine($"############ new high value error:{idType}");
        }

        var client = GrpcClientFactory.CreateIdGeneratorServiceClient(_options.Value.IdGeneratorGrpcServiceUrl);
        var r = await client
            .GenerateNextHighValueAsync(new GenerateNextHighValueRequest { IdType = (int)idType, IdKey = key }, cancellationToken: cancellationToken)
            .ResponseAsync.ConfigureAwait(false);
        return r.HighValue;
    }
}
