// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Logs out the user.
/// See <a href="https://corefork.telegram.org/method/auth.logOut" />
///</summary>
internal sealed class LogOutHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestLogOut, MyTelegram.Schema.Auth.ILoggedOut>,
    Auth.ILogOutHandler, IProcessedHandler
{
    private readonly IOptionsMonitor<MyTelegramMessengerServerOptions> _options;
    private readonly ICacheManager<FutureAuthTokenCacheItem> _cacheManager;
    private readonly IRandomHelper _randomHelper;
    private readonly IHashHelper _hashHelper;
    private readonly IEventBus _eventBus;
    private readonly int _futureAuthTokenExpirationDays = 7;
    public LogOutHandler(IOptionsMonitor<MyTelegramMessengerServerOptions> options, ICacheManager<FutureAuthTokenCacheItem> cacheManager, IRandomHelper randomHelper, IHashHelper hashHelper, IEventBus eventBus)
    {
        _options = options;
        _cacheManager = cacheManager;
        _randomHelper = randomHelper;
        _hashHelper = hashHelper;
        _eventBus = eventBus;
    }

    protected override async Task<ILoggedOut> HandleCoreAsync(IRequestInput input,
        RequestLogOut obj)
    {
        var r = new TLoggedOut();
        if (_options.CurrentValue.EnableFutureAuthToken)
        {
            var futureAuthTokenBytes = new byte[64];
            _randomHelper.NextBytes(futureAuthTokenBytes);
            var authTokenId = BitConverter.ToString(_hashHelper.Sha1(futureAuthTokenBytes)).Replace("-", string.Empty);
            var cacheKey =FutureAuthTokenCacheItem.GetCacheKey(authTokenId);
            await _cacheManager.SetAsync(cacheKey, new FutureAuthTokenCacheItem(input.UserId, authTokenId), (int)TimeSpan.FromDays(_futureAuthTokenExpirationDays).TotalSeconds);
            r.FutureAuthToken = futureAuthTokenBytes;
        }

        await _eventBus.PublishAsync(new UserLoggedOutEvent(input.UserId, input.AuthKeyId, input.PermAuthKeyId));

        return r;
    }
}
