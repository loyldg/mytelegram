using Microsoft.Cuckoo;

namespace MyTelegram.MessengerServer.Services.Filters;

public class MyCuckooFilter : ICuckooFilter
{
    private readonly Microsoft.Cuckoo.ICuckooFilter _filter = new CuckooFilter(5_000_000, 0.01);

    public Task<bool> ExistsAsync(byte[] filterKey)
    {
        return Task.FromResult(_filter.Contains(filterKey));
    }

    public Task<bool> AddAsync(byte[] filterKey)
    {
        return Task.FromResult(_filter.TryInsert(filterKey));
    }

    public Task<bool> DeleteAsync(byte[] filterKey)
    {
        return Task.FromResult(_filter.Remove(filterKey));
    }
}
