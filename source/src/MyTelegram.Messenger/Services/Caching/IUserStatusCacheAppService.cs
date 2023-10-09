namespace MyTelegram.Messenger.Services.Caching;

public interface IUserStatusCacheAppService
{
    IUserStatus GetUserStatus(long userId);

    void UpdateStatus(long userId,
        bool online);
}