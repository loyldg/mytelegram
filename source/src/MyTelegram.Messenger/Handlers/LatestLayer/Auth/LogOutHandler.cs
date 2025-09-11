namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;

///<summary>
/// Logs out the user.
/// See <a href="https://corefork.telegram.org/method/auth.logOut" />
///</summary>
internal sealed class LogOutHandler(
    IOptionsMonitor<MyTelegramMessengerServerOptions> options,
    ICacheManager<FutureAuthTokenCacheItem> cacheManager,
    IRandomHelper randomHelper,
    IHashHelper hashHelper,
    IEventBus eventBus)
    : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestLogOut, MyTelegram.Schema.Auth.ILoggedOut>
{
    private readonly int _futureAuthTokenExpirationDays = 7;

    protected override async Task<ILoggedOut> HandleCoreAsync(IRequestInput input,
        RequestLogOut obj)
    {
        var r = new TLoggedOut();
        if (options.CurrentValue.EnableFutureAuthToken)
        {
            var futureAuthTokenBytes = new byte[64];
            randomHelper.NextBytes(futureAuthTokenBytes);
            var authTokenId = BitConverter.ToString(hashHelper.Sha1(futureAuthTokenBytes)).Replace("-", string.Empty);
            var cacheKey =FutureAuthTokenCacheItem.GetCacheKey(authTokenId);
            await cacheManager.SetAsync(cacheKey, new FutureAuthTokenCacheItem(input.UserId, authTokenId), (int)TimeSpan.FromDays(_futureAuthTokenExpirationDays).TotalSeconds);
            r.FutureAuthToken = futureAuthTokenBytes;
        }

        await eventBus.PublishAsync(new UserLoggedOutEvent(input.UserId, input.AuthKeyId, input.PermAuthKeyId));

        return r;
    }
}
