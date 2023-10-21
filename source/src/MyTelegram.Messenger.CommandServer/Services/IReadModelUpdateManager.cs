namespace MyTelegram.Messenger.CommandServer.Services;

public interface IReadModelUpdateManager
{
    Task<UpdateStrategy> GetReadModelUpdateStrategyAsync<TReadModel>();
}