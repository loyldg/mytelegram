// ReSharper disable once CheckNamespace

namespace MyTelegram;

public interface IIdGenerator
{
    Task<int> NextIdAsync(IdType idType,
        long id, int step = 1, CancellationToken cancellationToken = default);

    Task<long> NextLongIdAsync(IdType idType,
        long id = 0, int step = 1, CancellationToken cancellationToken = default);

    long GetInitialId(IdType idType);
    long GetSequence(IdType idType, long id);
}