using System.Collections.Concurrent;

namespace MyTelegram.Domain;

public class SimpleInMemoryIdGenerator : IIdGenerator
{
    private readonly ConcurrentDictionary<string, long> _ids = new();
    public async Task<int> NextIdAsync(IdType idType,
        long id,
        int step = 1,
        CancellationToken cancellationToken = default)
    {
        var value = await NextLongIdAsync(idType, id, step, cancellationToken);

        return (int)value;
    }

    public Task<long> NextLongIdAsync(IdType idType,
        long id = 0,
        int step = 1,
        CancellationToken cancellationToken = default)
    {
        var key = $"{idType}_{id}";
        if (_ids.TryGetValue(key, out var value))
        {
            _ids.TryUpdate(key, value + step, value);
            return Task.FromResult(value + step);
        }

        var initialValue = GetInitialId(idType) + step;
        _ids.TryAdd(key, initialValue);

        return Task.FromResult(initialValue);
    }

    public long GetInitialId(IdType idType)
    {
        return idType switch
        {
            IdType.ChannelId => MyTelegramConsts.ChannelIdBase + 100_000,
            IdType.UserId => MyTelegramConsts.UserIdBase + 100_000,
            IdType.BotUserId => MyTelegramConsts.BotUserIdBase + 100_000,
            IdType.ChatId => MyTelegramConsts.ChatIdBase + 100_000,
            IdType.Pts => MyTelegramConsts.PtsIdBase,
            IdType.FolderId => MyTelegramConsts.FolderIdBase,
            _ => 0
        };
    }

    public long GetSequence(IdType idType, long id)
    {
        return id - GetInitialId(idType);
    }
}
