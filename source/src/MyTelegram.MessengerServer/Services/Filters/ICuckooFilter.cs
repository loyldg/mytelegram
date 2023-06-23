namespace MyTelegram.MessengerServer.Services.Filters;

public interface ICuckooFilter
{
    Task<bool> AddAsync(byte[] value);
    Task<bool> DeleteAsync(byte[] value);
    Task<bool> ExistsAsync(byte[] value);
}