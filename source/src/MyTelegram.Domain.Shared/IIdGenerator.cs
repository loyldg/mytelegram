// ReSharper disable once CheckNamespace
namespace MyTelegram;

public interface IIdGenerator
{
    Task<int> NextIdAsync(IdType idType,
        long id, int step = 1, CancellationToken cancellationToken = default);

    Task<long> NextLongIdAsync(IdType idType,
        long id = 0, int step = 1, CancellationToken cancellationToken = default);
}

public interface IClock
{
    /// <summary>
    /// Gets Now.
    /// </summary>
    DateTime Now { get; }

    /// <summary>
    /// Gets kind.
    /// </summary>
    DateTimeKind Kind { get; }
}
