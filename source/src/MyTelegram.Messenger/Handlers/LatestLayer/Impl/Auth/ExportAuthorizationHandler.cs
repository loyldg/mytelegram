// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Returns data for copying authorization to another data-center.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 DC_ID_INVALID The provided DC ID is invalid.
/// See <a href="https://corefork.telegram.org/method/auth.exportAuthorization" />
///</summary>
internal sealed class ExportAuthorizationHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestExportAuthorization, MyTelegram.Schema.Auth.IExportedAuthorization>,
    Auth.IExportAuthorizationHandler
{
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
        MyTelegram.Schema.Auth.RequestExportAuthorization obj)
    {
        var dataCenter = _options.Value.DcOptions.FirstOrDefault(p => p.Id == obj.DcId);
        if (dataCenter == null)
        {
            //throw new BadRequestException("DC_ID_INVALID");
            RpcErrors.RpcErrors400.DcIdInvalid.ThrowRpcError();
        }

        var bytes = new byte[128];
        _randomHelper.NextBytes(bytes);
        var keyBytes = _hashHelper.Sha1(bytes);
        var key = BitConverter.ToString(keyBytes).Replace("-", string.Empty);
        var cacheKey = MyCacheKey.With("authorizations", key);
        var cacheSeconds = 600;//10m
        await _cacheManager.SetAsync(cacheKey, input.UserId.ToString(), cacheSeconds);

        return new TExportedAuthorization { Bytes = bytes, Id = input.UserId };
    }
}
