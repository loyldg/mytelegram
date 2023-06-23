namespace MyTelegram.MessengerServer.Services.Caching;

public interface ILoginTokenCacheAppService
{
    void AddLoginSuccessAuthKeyIdToCache(long authKeyId,
        long userId);

    bool TryGetCachedLoginInfo(long authKeyId,
        [NotNullWhen(true)] out CacheLoginToken? loginTokenCache);

    bool TryRemoveLoginInfo(long authKeyId,
        [NotNullWhen(true)] out CacheLoginToken? loginTokenCache);
}