namespace MyTelegram.Messenger.Services.Filters;

public interface IInMemoryFilterDataLoader
{
    Task LoadAllFilterDataAsync();
}