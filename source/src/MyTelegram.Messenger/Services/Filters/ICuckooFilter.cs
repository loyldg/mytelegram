namespace MyTelegram.Messenger.Services.Filters;

public interface ICuckooFilter
{
    Task<bool> ExistsAsync(byte[] value);

    Task<bool> AddAsync(byte[] value);
    Task<bool> DeleteAsync(byte[] value);
}