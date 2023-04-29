namespace MyTelegram.MessengerServer.Handlers.Impl;

public class GetFutureSaltsHandler : BaseObjectHandler<RequestGetFutureSalts, IFutureSalts>,
    IGetFutureSaltsHandler, IProcessedHandler
{
    //private readonly IDistributedCache<List<CachedFutureSalt>> _distributedCache;
    private readonly ICacheManager<List<CachedFutureSalt>> _cacheManager;
    private readonly ILogger<GetFutureSaltsHandler> _logger;
    private readonly IRandomHelper _randomHelper;

    public GetFutureSaltsHandler(
        IRandomHelper randomHelper,
        ICacheManager<List<CachedFutureSalt>> cacheManager,
        ILogger<GetFutureSaltsHandler> logger)
    {
        _randomHelper = randomHelper;
        _cacheManager = cacheManager;
        _logger = logger;
    }

    protected override async Task<IFutureSalts> HandleCoreAsync(IRequestInput input,
        RequestGetFutureSalts obj)
    {
        var salts = await GetOrCreateFutureSaltsAsync(input.UserId, input.AuthKeyId, obj.Num);
        var r = new TFutureSalts
        {
            Now = CurrentDate,
            ReqMsgId = input.ReqMsgId,
            Salts = new TVector<IFutureSalt>(salts.Select(p =>
                new TFutureSalt { Salt = p.Salt, ValidSince = p.ValidSince, ValidUntil = p.ValidUntil }))
        };

        return r;
    }

    private async Task<List<CachedFutureSalt>> GetOrCreateFutureSaltsAsync(long userId,
        long authKeyId,
        int count)
    {
        var key = $"future_salt_{userId}_{authKeyId}";
        var cachedSalts = await _cacheManager.GetAsync(key);
        if (cachedSalts == null)
        {
            cachedSalts = new List<CachedFutureSalt>();
            var now = DateTime.UtcNow.ToTimestamp();
            var validUntil = DateTime.UtcNow.AddHours(2).ToTimestamp();
            var maxCount = Math.Min(8, count);
            for (var i = 0; i < maxCount; i++)
            {
                cachedSalts.Add(new CachedFutureSalt(_randomHelper.NextLong(), now, validUntil));
            }

            //Logger.LogDebug($"create future salt,count:{maxCount}");
            _logger.LogDebug("UserId={UserId} new server salt created:{@ServerSalt}", userId, cachedSalts);
            await _cacheManager.SetAsync(key,
                cachedSalts,
                (int)TimeSpan.FromHours(2).TotalSeconds);
        }

        return cachedSalts;
    }
}

//[CacheName("CachedFutureSalt")]
public record CachedFutureSalt(long Salt,
    int ValidSince,
    int ValidUntil);