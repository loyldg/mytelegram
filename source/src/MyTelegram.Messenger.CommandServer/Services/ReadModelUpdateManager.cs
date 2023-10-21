namespace MyTelegram.Messenger.CommandServer.Services;

public class ReadModelUpdateManager : IReadModelUpdateManager
{
    public Task<UpdateStrategy> GetReadModelUpdateStrategyAsync<TReadModel>()
    {
        return Task.FromResult(UpdateStrategy.UpdateDatabase);
    }
}