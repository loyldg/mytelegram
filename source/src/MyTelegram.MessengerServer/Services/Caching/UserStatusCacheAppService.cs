namespace MyTelegram.MessengerServer.Services.Caching;

public class UserStatusCacheAppService : IUserStatusCacheAppService
{
    private readonly IInMemoryRepository<UserStatus, long> _inMemoryRepository;

    public UserStatusCacheAppService(IInMemoryRepository<UserStatus, long> inMemoryRepository)
    {
        _inMemoryRepository = inMemoryRepository;
    }

    public void UpdateStatus(long userId,
        bool online)
    {
        var item = _inMemoryRepository.Find(userId);
        if (item == null)
        {
            item = new UserStatus(userId, online);
            _inMemoryRepository.Insert(userId, item);
        }
        else
        {
            item.UpdateStatus(online);
        }
    }

    public IUserStatus GetUserStatus(long userId)
    {
        var status = _inMemoryRepository.Find(userId);
        if (status == null)
        {
            return new TUserStatusEmpty();
        }

        return GetUserStatus(status.LastUpdateDate, status.Online);
    }

    private static IUserStatus GetUserStatus(DateTime lastUpdateUtcTime,
        bool isOnline)
    {
        var timespan = (DateTime.UtcNow - lastUpdateUtcTime).TotalSeconds;
        var wasOnline = lastUpdateUtcTime.ToTimestamp();
        var expire = lastUpdateUtcTime.AddMinutes(5).ToTimestamp();
        const int day = 60 * 60 * 24;
        IUserStatus status;
        if (isOnline)
        {
            status = timespan switch
            {
                < 60 => new TUserStatusOnline { Expires = expire },
                > 60 and < 60 * 7 => new TUserStatusOffline { WasOnline = wasOnline },
                < day * 1 => new TUserStatusRecently(),
                < day * 14 => new TUserStatusLastWeek(),
                < day * 30 => new TUserStatusLastMonth(),
                _ => new TUserStatusEmpty()
            };
        }
        else
        {
            status = new TUserStatusOffline { WasOnline = wasOnline };
        }

        return status;
    }
}