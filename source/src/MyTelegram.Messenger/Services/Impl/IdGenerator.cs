using MyTelegram.EventFlow;
using MyTelegram.Services.Services.IdGenerator;

namespace MyTelegram.Messenger.Services.Impl;

public class IdGenerator(
    IHiLoValueGeneratorCache cache,
    IHiLoValueGeneratorFactory factory,
    IQueryProcessor queryProcessor,
    IQueryFilterScope queryFilterScope,
    IHiLoStateBlockSizeHelper stateBlockSizeHelper,
    //IIdMappingService idMappingService,
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

        return nextId + GetInitialId(idType);
    }

    public long GetSequence(IdType idType, long id)
    {
        return id - GetInitialId(idType);
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
            //IdType.UserSequence => 100,
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

    private async Task<HiLoValueGeneratorState> GetStateAsync(
        IdType idType,
        long id,
        long oldMaxId)
    {
        if (oldMaxId > 0)
        {
            var blockSize = stateBlockSizeHelper.GetBlockSize(idType);
            var high = oldMaxId / blockSize;

            return await cache.GetOrAddAsync(
                idType,
                id,
                () => Task.FromResult(
                    new HiLoValueGeneratorState(
                        blockSize,
                        oldMaxId,
                        (high + 1) * blockSize)));
        }

        return cache.GetOrAdd(idType, id);
    }

    private async Task<HiLoValueGeneratorState> GetMessageIdStateAsync(IdType idType, long id)
    {
        var maxId = await GetMaxMessageIdAsync(id);
        if (maxId > 0)
        {
            var blockSize = stateBlockSizeHelper.GetBlockSize(idType);
            var high = maxId / blockSize;
            return await cache.GetOrAddAsync(idType, id, () => Task.FromResult(new HiLoValueGeneratorState(blockSize, maxId, (high + 1) * blockSize + 1)));

            //var aggregate = new MessageAggregate(MessageId.Create(id, maxId + 1));
            //await aggregate.LoadAsync(eventStore, snapshotStore, CancellationToken.None);
            //if (aggregate.IsNew)
            //{
            //    var blockSize = stateBlockSizeHelper.GetBlockSize(idType);
            //    var high = maxId / blockSize;
            //    return await cache.GetOrAddAsync(idType, id, () => Task.FromResult(new HiLoValueGeneratorState(blockSize, maxId, (high + 1) * blockSize + 1)));
            //}
        }

        return cache.GetOrAdd(idType, id);
    }
}