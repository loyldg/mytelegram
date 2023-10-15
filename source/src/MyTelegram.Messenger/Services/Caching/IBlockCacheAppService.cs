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

public class BlockCacheAppService : IBlockCacheAppService
{
    public Task BlockAsync(long userId, long targetPeerId)
    {
        return Task.CompletedTask;
    }

    public Task<bool> IsBlockedAsync(long userId, long targetPeerId)
    {
        return Task.FromResult(false);
    }

    public Task UnblockAsync(long userId, long targetPeerId)
    {
        return Task.CompletedTask;
    }
}