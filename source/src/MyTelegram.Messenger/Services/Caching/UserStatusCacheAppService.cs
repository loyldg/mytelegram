namespace MyTelegram.Messenger.Services.Caching;

public class UserStatusCacheAppService(ICacheManager<UserStatusCacheItem> cacheManager) : IUserStatusCacheAppService, ISingletonDependency
{
    private readonly ConcurrentDictionary<long, UserStatus> _userStatusList = [];
    private const int OnlineTimeoutSeconds = 120;
    private bool _isTimerStarted = false;


    public async Task<IUserStatus> GetUserStatusAsync(long userId)
    {
        if (!_userStatusList.TryGetValue(userId, out var status))
        {
            var cachedItem = await cacheManager.GetAsync(UserStatusCacheItem.GetCacheKey(userId));
            if (cachedItem != null)
            {
                return GetUserStatus(cachedItem.LastUpdateDate, cachedItem.IsOnline);
            }
            {
                return new TUserStatusEmpty();
            }
        }

        return GetUserStatus(status.LastUpdateDate, status.Online);
    }

    public async Task UpdateStatusAsync(long userId, bool online)
    {
        if (!_userStatusList.TryGetValue(userId, out var status))
        {
            status = new UserStatus(userId, online);
            _userStatusList.TryAdd(userId, status);
        }
        else
        {
            status.UpdateStatus(online);
        }

        await cacheManager.SetAsync(UserStatusCacheItem.GetCacheKey(userId), new UserStatusCacheItem(userId, online, DateTime.UtcNow.ToTimestamp()));

#if DEBUG
        UpdateOnlineCount();
        if (!_isTimerStarted)
        {
            _isTimerStarted = true;
            _ = Task.Run(async () =>
            {
                UpdateOnlineCount();
                await Task.Delay(15);
            });
        }
#endif
    }
    private void UpdateOnlineCount()
    {
        var (onlineCount1, onlineCount2) = GetOnlineCount();
        var title =
            $"MyTelegram query server, layer: {MyTelegramConsts.Layer}  Online count: {onlineCount1}({onlineCount2}), Offline Count: {_userStatusList.Count - onlineCount1} ,Total Count: {_userStatusList.Count}";
        Console.Title = title;
    }

    public (int onlineCount1, int onlineCount2) GetOnlineCount()
    {
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        int count = 0;
        int onlineCount2 = 0;

        foreach (var kv in _userStatusList)
        {
            if (kv.Value.Online)
            {
                onlineCount2++;

                if (now - kv.Value.LastUpdateDate <= OnlineTimeoutSeconds)
                {
                    count++;
                }
            }
        }

        return (count, onlineCount2);
    }

    private static IUserStatus GetUserStatus(int lastUpdateUtcTime,
        bool isOnline)
    {
        var now = DateTime.UtcNow.ToTimestamp();
        var timespan = now - lastUpdateUtcTime;
        var wasOnline = lastUpdateUtcTime;
        var expire = lastUpdateUtcTime + 300;// 5 minutes
        const int day = 60 * 60 * 24;
        if (isOnline)
        {
            if (timespan < 60)
            {
                return new TUserStatusOnline { Expires = expire };
            }
        }

        IUserStatus status = timespan switch
        {
            < 60 => new TUserStatusOffline { WasOnline = expire },
            > 60 and < 60 * 7 => new TUserStatusOffline { WasOnline = wasOnline },
            < day * 1 => new TUserStatusRecently(),
            < day * 14 => new TUserStatusLastWeek(),
            < day * 30 => new TUserStatusLastMonth(),
            _ => new TUserStatusEmpty()
        };

        return status;
    }
}