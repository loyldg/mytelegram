namespace MyTelegram.Messenger.Services.Impl;

/// <summary>
/// https://corefork.telegram.org/api/offsets#hash-generation
/// </summary>
public class HashCalculator : IHashCalculator
{
    public long GetHash(IEnumerable<long> ids)
    {
        var hash = 0L;
        foreach (var id in ids)
        {
            hash ^= (hash >> 21);
            hash ^= (hash << 35);
            hash ^= (hash >> 4);
            hash += id;
        }

        return hash;
    }
}