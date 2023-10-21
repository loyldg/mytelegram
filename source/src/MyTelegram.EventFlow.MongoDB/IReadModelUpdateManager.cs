namespace MyTelegram.EventFlow.MongoDB;

public interface IReadModelUpdateManager
{
    Task<UpdateStrategy> GetReadModelUpdateStrategyAsync<TReadModel>();
}