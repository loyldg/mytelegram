namespace MyTelegram.Messenger.Services.Caching;

public interface ICachedReadModelLoader
{
    Task LoadAsync<TReadModel>();
}