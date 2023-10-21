namespace MyTelegram.EventFlow.MongoDB;

public interface IReadModelUpdateStrategy
{
    Task<bool> ShouldUpdateReadModelAsync<TReadModel>();
}