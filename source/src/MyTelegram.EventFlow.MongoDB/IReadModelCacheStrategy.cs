namespace MyTelegram.EventFlow.MongoDB;

public interface IReadModelCacheStrategy
{
    Task<bool> ShouldCacheReadModelAsync<TReadModel>();
}