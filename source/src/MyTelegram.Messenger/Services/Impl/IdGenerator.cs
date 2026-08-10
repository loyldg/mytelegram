using MyTelegram.EventFlow;
using MyTelegram.Services.Services.IdGenerator;

namespace MyTelegram.Messenger.Services.Impl;

public class IdGenerator(
    IHiLoValueGeneratorCache cache,
    IHiLoValueGeneratorFactory factory,
    IQueryProcessor queryProcessor,
    IQueryFilterScope queryFilterScope,
    IHiLoStateBlockSizeHelper stateBlockSizeHelper,
    ILogger<IdGenerator> logger)
    : IIdGenerator, ITransientDependency
{
    public async Task<int> NextIdAsync(IdType idType,
        long id,
        int step = 1,
        CancellationToken cancellationToken = default)
    {
        return (int)await NextLongIdAsync(idType, id, step, cancellationToken);
    }

    public async Task<long> NextLongIdAsync(IdType idType,
        long id = 0,
        int step = 1,
        CancellationToken cancellationToken = default)
    {
        var sw = Stopwatch.StartNew();

        HiLoValueGeneratorState? state = null;
        switch (idType)
        {
            case IdType.MessageId:
                if (!cache.Exists(idType, id))
                {
                    var maxMessageId = await GetMaxMessageIdAsync(id);
                    state = await GetStateAsync(idType, id, maxMessageId);
                }
                break;
            case IdType.UserId:
                if (!cache.Exists(idType, id))
                {
                    var maxUserId = await GetMaxUserIdAsync();
                    if (maxUserId > 0)
                    {
                        maxUserId = maxUserId - MyTelegramConsts.UserIdInitId;
                        maxUserId = Math.Max(maxUserId, 0);
                    }
                    state = await GetStateAsync(idType, id, maxUserId);
                }
                break;
            case IdType.ChannelId:
                {
                    if (!cache.Exists(idType, id))
                    {
                        var maxChannelId = await GetMaxChannelIdAsync();
                        if (maxChannelId > 0)
                        {
                            maxChannelId = maxChannelId - MyTelegramConsts.ChannelInitId;
                            maxChannelId = Math.Max(maxChannelId, 0);
                        }
                        state = await GetStateAsync(idType, id, maxChannelId);
                    }
                }
                break;
        }

        state ??= cache.GetOrAdd(idType, id);

        var generator = factory.Create(state);
        var nextId = await generator.NextAsync(idType, id, cancellationToken);
        sw.Stop();

        if (sw.Elapsed.TotalMilliseconds > 100)
        {
            logger.LogWarning("[{Timespan}] Generate id too slow, idType: {IdType}, id: {Id}", sw.Elapsed, idType, id);
        }

        return nextId + GetInitId(idType);
    }

    private static long GetInitId(IdType idType)
    {
        return idType switch
        {
            IdType.ChannelId => MyTelegramConsts.ChannelInitId,
            IdType.UserId => MyTelegramConsts.UserIdInitId + 10000, // First 10000 for testing
            IdType.BotUserId => MyTelegramConsts.BotUserInitId,
            IdType.ChatId => MyTelegramConsts.ChatIdInitId,
            IdType.Pts => MyTelegramConsts.PtsInitId,
            IdType.FolderId => MyTelegramConsts.FolderInitId,
            _ => 0
        };
    }

    private async Task<long> GetMaxChannelIdAsync()
    {
        using (queryFilterScope.DisableSoftDelete())
        {
            var id = await queryProcessor.ProcessAsync(new GetMaxChannelIdQuery());

            if (id > 0)
            {
                return id;
            }

            return 0;
        }
    }

    private async Task<long> GetMaxUserIdAsync()
    {
        using (queryFilterScope.DisableSoftDelete())
        {
            var id = await queryProcessor.ProcessAsync(new GetMaxUserIdQuery());

            if (id > 0)
            {
                return id;
            }

            return 0;
        }
    }

    private async Task<int> GetMaxMessageIdAsync(long ownerPeerId)
    {
        using (queryFilterScope.DisableSoftDelete())
        {
            int? maxId = await queryProcessor.ProcessAsync(new GetMaxMessageIdByPeerIdQuery(ownerPeerId));

            return maxId ?? 0;
        }
    }

    private async Task<HiLoValueGeneratorState> GetStateAsync(IdType idType, long id, long oldMaxId)
    {
        if (oldMaxId > 0)
        {
            var blockSize = stateBlockSizeHelper.GetBlockSize(idType);
            var high = oldMaxId / blockSize;
            return await cache.GetOrAddAsync(idType, id, () => Task.FromResult(new HiLoValueGeneratorState(blockSize, oldMaxId, (high + 1) * blockSize + 1)));
        }

        return cache.GetOrAdd(idType, id);
    }
}