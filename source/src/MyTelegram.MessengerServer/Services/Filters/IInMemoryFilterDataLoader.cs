namespace MyTelegram.MessengerServer.Services.Filters;

public interface IInMemoryFilterDataLoader
{
    Task LoadAllFilterDataAsync();
}
