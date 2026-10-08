namespace MyTelegram.Messenger.Services.Caching;

public interface IUserStatusCacheAppService
{
    //IUserStatus GetUserStatus(long userId);

    Task<IUserStatus> GetUserStatusAsync(long userId);


    //void UpdateStatus(long userId, bool online);

    Task UpdateStatusAsync(long userId,
        bool online);
}