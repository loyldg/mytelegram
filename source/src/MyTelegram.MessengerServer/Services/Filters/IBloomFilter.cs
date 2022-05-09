namespace MyTelegram.MessengerServer.Services.Filters;

public interface IBloomFilter
{
    Task<bool> ExistsAsync(byte[] value);
    Task<bool> AddAsync(byte[] value);

    Task<IList<bool>> ExistsAsync(IEnumerable<byte[]> values);

    Task<IList<bool>> AddAsync(IEnumerable<byte[]> values);
}