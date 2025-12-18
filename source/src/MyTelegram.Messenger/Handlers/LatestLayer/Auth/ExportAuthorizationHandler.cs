namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;
/// <summary>
/// Returns data for copying authorization to another data-center.
/// Possible errors
/// Code Type Description
/// 400 DC_ID_INVALID The provided DC ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/auth.exportAuthorization"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class ExportAuthorizationHandler(IOptions<MyTelegramMessengerServerOptions> options, IRandomHelper randomHelper, IHashHelper hashHelper, ILogger<ExportAuthorizationHandler> logger, ICacheManager<AuthorizationCacheItem> cacheManager) : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestExportAuthorization, MyTelegram.Schema.Auth.IExportedAuthorization>
{
    //private readonly IDistributedCache<string> _distributedCache;
    protected override async Task<IExportedAuthorization> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestExportAuthorization obj)
    {
        //logger.LogInformation("ExportAuthorizationHandler:{@Input},data:{@data}", input, obj);
        var dataCenter = options.Value.DcOptions.FirstOrDefault(p => p.Id == obj.DcId);
        if (dataCenter == null)
        {
            //throw new BadRequestException("DC_ID_INVALID");
            RpcErrors.RpcErrors400.DcIdInvalid.ThrowRpcError();
        }

        var bytes = new byte[128];
        randomHelper.NextBytes(bytes);
        var keyBytes = hashHelper.Sha1(bytes);
        var key = BitConverter.ToString(keyBytes).Replace("-", string.Empty);
        //var cacheKey = MyCacheKey.With("authorizations", key);
        var cacheKey = AuthorizationCacheItem.GetCacheKey(key);
        var cacheItem = new AuthorizationCacheItem(input.UserId, input.AccessHashKeyId);
        var cacheSeconds = 600; //10m
        await cacheManager.SetAsync(cacheKey, cacheItem, cacheSeconds);
        return new TExportedAuthorization
        {
            Bytes = bytes,
            Id = input.UserId
        };
    }
}