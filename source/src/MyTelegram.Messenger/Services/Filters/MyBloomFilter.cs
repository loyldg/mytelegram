//using BloomFilter;

using Microsoft.Cuckoo;
using Task = System.Threading.Tasks.Task;

namespace MyTelegram.Messenger.Services.Filters;

public class MyBloomFilter : IBloomFilter
{
    //private readonly BloomFilter.IBloomFilter _filter = FilterBuilder.Build(5_000_000, 0.001, HashMethod.LCGWithFNV1a);
    private readonly Microsoft.Cuckoo.ICuckooFilter _filter = new CuckooFilter(50_000_000, 0.001);
    public Task<bool> ExistsAsync(byte[] value)
    {
        //return _filter.ContainsAsync(value);
        return Task.FromResult(_filter.Contains(value));
    }

    public Task<bool> AddAsync(byte[] value)
    {
        //return _filter.AddAsync(value);
        return Task.FromResult(_filter.TryInsert(value));
    }

    public Task<IList<bool>> ExistsAsync(IEnumerable<byte[]> values)
    {
        //return _filter.ContainsAsync(values);
        var results = new List<bool>();
        foreach (var value in values)
        {
            results.Add(_filter.Contains(value));
        }

        return Task.FromResult<IList<bool>>(results);
    }

    public Task<IList<bool>> AddAsync(IEnumerable<byte[]> values)
    {
        //return _filter.AddAsync(values);
        var results = new List<bool>();
        foreach (var value in values)
        {
            results.Add(_filter.TryInsert(value));
        }

        return Task.FromResult<IList<bool>>(results);
    }
}