namespace MyTelegram.MessengerServer.Abp.Services;

public class HiLoHighValueGenerator : IHiLoHighValueGenerator, ITransientDependency
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
        var client = GrpcClientFactory.CreateIdGeneratorServiceClient(_options.Value.IdGeneratorRpcServiceUrl);
        var r = await client
            .GenerateNextHighValueAsync(new GenerateNextHighValueRequest { IdType = (int)idType, IdKey = key }, cancellationToken: cancellationToken)
            .ResponseAsync.ConfigureAwait(false);
        return r.HighValue;
    }
}
