namespace MyTelegram.MessengerServer.Services.Caching;

public class LoginTokenCacheAppService : ILoginTokenCacheAppService
{
    private readonly IInMemoryRepository<CacheLoginToken, long> _inMemoryRepository;

    public LoginTokenCacheAppService(IInMemoryRepository<CacheLoginToken, long> inMemoryRepository)
    {
        _inMemoryRepository = inMemoryRepository;
    }

    public void AddLoginSuccessAuthKeyIdToCache(long authKeyId,
        long userId)
    {
        _inMemoryRepository.Insert(authKeyId, new CacheLoginToken(authKeyId, userId));
    }

    public bool TryGetCachedLoginInfo(long authKeyId,
        [NotNullWhen(true)] out CacheLoginToken? loginTokenCache)
    {
        loginTokenCache = _inMemoryRepository.Find(authKeyId);

        return loginTokenCache != null;
    }

    public bool TryRemoveLoginInfo(long authKeyId,
        [NotNullWhen(true)] out CacheLoginToken? loginTokenCache)
    {
        return _inMemoryRepository.TryDelete(authKeyId, out loginTokenCache);
    }
}
