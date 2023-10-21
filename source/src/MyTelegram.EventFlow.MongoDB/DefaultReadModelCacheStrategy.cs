namespace MyTelegram.EventFlow.MongoDB;

public class DefaultReadModelCacheStrategy : IReadModelCacheStrategy
{
    public Task<bool> ShouldCacheReadModelAsync<TReadModel>()
    {
        return Task.FromResult(false);
    }
}