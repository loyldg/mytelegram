namespace MyTelegram.Messenger.Services.Caching;

public interface IBlockCacheAppService
{
    Task BlockAsync(long userId,
        long targetPeerId);

    //Task<bool> IsBlockedAsync(long userId,
    //    int targetPeerId);
    Task<bool> IsBlockedAsync(long userId,
        long targetPeerId);

    Task UnblockAsync(long userId,
        long targetPeerId);

    //Task LoadAllBlockedAsync();
}