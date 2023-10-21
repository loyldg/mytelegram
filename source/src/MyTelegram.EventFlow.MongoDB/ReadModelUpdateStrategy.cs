namespace MyTelegram.EventFlow.MongoDB;

public class ReadModelUpdateStrategy : IReadModelUpdateStrategy
{
    public Task<bool> ShouldUpdateReadModelAsync<TReadModel>()
    {
        return Task.FromResult(true);
    }
}