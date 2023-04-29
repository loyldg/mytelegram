using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class ExportAuthorizationHandler : RpcResultObjectHandler<RequestExportAuthorization, IExportedAuthorization>,
    IExportAuthorizationHandler, IProcessedHandler
{
    //private readonly IDistributedCache<string> _distributedCache;
    private readonly ICacheManager<string> _cacheManager;
    private readonly IHashHelper _hashHelper;
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;
    private readonly IRandomHelper _randomHelper;

    public ExportAuthorizationHandler(IOptions<MyTelegramMessengerServerOptions> options,
        IRandomHelper randomHelper,
        IHashHelper hashHelper,
        ICacheManager<string> cacheManager)
    {
        _options = options;
        _randomHelper = randomHelper;
        _hashHelper = hashHelper;
        _cacheManager = cacheManager;
    }

    protected override async Task<IExportedAuthorization> HandleCoreAsync(IRequestInput input,
        RequestExportAuthorization obj)
    {
        var dataCenter = _options.Value.DcOptions.FirstOrDefault(p => p.Id == obj.DcId);
        if (dataCenter == null)
        {
            throw new BadRequestException("DC_ID_INVALID");
        }

        var bytes = new byte[128];
        _randomHelper.NextBytes(bytes);
        var keyBytes = _hashHelper.Sha1(bytes);
        var key = BitConverter.ToString(keyBytes).Replace("-", string.Empty);
        await _cacheManager.SetAsync(key, input.UserId.ToString());

        return new TExportedAuthorization { Bytes = bytes, Id = input.UserId };
    }
}
