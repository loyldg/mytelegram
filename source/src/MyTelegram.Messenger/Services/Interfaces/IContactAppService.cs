namespace MyTelegram.Messenger.Services.Interfaces;

public interface IContactAppService
{
    Task<SearchContactOutput> SearchAsync(long selfUserId,
        string keyword);
}