namespace MyTelegram.MessengerServer.Services.Filters;

public interface IBloomFilter
{
    ValueTask<bool> AddAsync(byte[] value);

    ValueTask<IList<bool>> AddAsync(IEnumerable<byte[]> values);
    ValueTask<bool> ExistsAsync(byte[] value);

    ValueTask<IList<bool>> ExistsAsync(IEnumerable<byte[]> values);
}