using MyTelegram.Messenger.Services.Filters;
using System.Buffers.Binary;
using IMessageViews = MyTelegram.Schema.IMessageViews;
using TMessageViews = MyTelegram.Schema.TMessageViews;

namespace MyTelegram.Messenger.Services.Impl;

public class ChannelMessageViewsAppService(
    IQueryProcessor queryProcessor,
    ICommandBus commandBus,
    ILogger<ChannelMessageViewsAppService> logger)
    : IChannelMessageViewsAppService, ISingletonDependency
{
    // 1M views per day, and the data from the first day will be cleared after 3 days.
    // 1M entries take up more than 2.5 MB of memory
    // We can increase this value based on the number of users in the system.
    private static readonly SimpleCuckooFilter Filter1 = new();
    private static readonly SimpleCuckooFilter Filter2 = new();
    private static readonly SimpleCuckooFilter Filter3 = new();

    private static readonly string ViewsFilterFileName = "views";
    private static SimpleCuckooFilter _currentFilter = Filter1;
    private static int _currentIndex;

    public static void GenerateFilterKey(long selfUserId, long channelId, int messageId, Span<byte> key)
    {
        BinaryPrimitives.WriteInt64LittleEndian(key, selfUserId);
        BinaryPrimitives.WriteInt64LittleEndian(key.Slice(8), channelId);
        BinaryPrimitives.WriteInt32LittleEndian(key.Slice(16), messageId);
    }

    public async Task<IList<IMessageViews>> GetMessageViewsAsync(long selfUserId,
        long authKeyId,
        long channelId,
        List<int> messageIdList,
        bool increment = false)
    {
        var messageIdGreaterThanZeroList = messageIdList.Where(p => p > 0).ToList();

        var needIncrementMessageIdList = new List<int>();
        //Span<byte> key = stackalloc byte[20];
        var tempBytes = ArrayPool<byte>.Shared.Rent(20);
        var key = tempBytes.AsSpan(0, 20);
        try
        {
            foreach (var id in messageIdList)
            {
                GenerateFilterKey(selfUserId, channelId, id, key);
                var canIncrementViews = CanIncrementViews(key);
                if (canIncrementViews)
                {
                    _currentFilter.Add(key);
                    needIncrementMessageIdList.Add(id);
                }
            }
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(tempBytes);
        }

        var messageViews = (await queryProcessor
                    .ProcessAsync(new GetMessageViewsQuery(channelId, messageIdGreaterThanZeroList))
                    .ConfigureAwait(false))
                .ToDictionary(k => k.MessageId, v => v)
            ;

        foreach (var messageId in needIncrementMessageIdList)
        {
            try
            {
                var command = new IncrementViewsCommand(MessageId.Create(channelId, messageId));
                await commandBus.PublishAsync(command);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "IncrementViews failed");
            }
        }

        // 2) Read current views (one query)
        var viewsDict = (await queryProcessor
                .ProcessAsync(new GetMessageViewsQuery(channelId, ids), CancellationToken.None)
                .ConfigureAwait(false))
            .ToDictionary(v => v.MessageId);

        // 3) Increment for first-time viewers (robust, with optional rollback)
        if (increment && needInc.Count > 0)
            foreach (var mid in needInc)
                try
                {
                    await commandBus.PublishAsync(new IncrementViewsCommand(MessageId.Create(channelId, mid)))
                        .ConfigureAwait(false);
                }
                catch (DomainError)
                {
                }

        // 4) Replies & projection
        var linkedChannelId = await queryProcessor
            .ProcessAsync(new GetLinkedChannelIdQuery(channelId), CancellationToken.None)
            .ConfigureAwait(false);
        var repliesDict = (await queryProcessor
                .ProcessAsync(new GetRepliesQuery(channelId, messageIdList), CancellationToken.None)
                .ConfigureAwait(false))
            .ToDictionary(r => r.MessageId);

        var result = new List<IMessageViews>(messageIdList.Count);
        foreach (var messageId in messageIdList)
        {
            var addOne = increment && needInc.Contains(messageId);
            if (viewsDict.TryGetValue(messageId, out var views))
            {
                repliesDict.TryGetValue(messageId, out var reply);
                var recentRepliers = reply?.RecentRepliers?.Count > 0
                    ? reply.RecentRepliers.Select(p => p.ToPeer()).ToList()
                    : new List<IPeer>();

                result.Add(new TMessageViews
                {
                    Views = addOne ? views.Views + 1 : views.Views,
                    Replies = new TMessageReplies
                    {
                        ChannelId = reply?.CommentChannelId ?? linkedChannelId,
                        Comments = linkedChannelId.HasValue,
                        Replies = reply?.Replies ?? 0,
                        RepliesPts = reply?.RepliesPts ?? 0,
                        MaxId = reply?.MaxId,
                        RecentRepliers = [.. recentRepliers]
                    }
                });
            }
            else
            {
                result.Add(new TMessageViews { Views = addOne ? 1 : 0 });
            }
        }

        return result;
    }


    private async Task<bool> TryRegisterViewAsync(string redisKey, byte[] filterKey)
    {
        // Local check: if Cuckoo filter already saw it, skip
        if (await cuckooFilter.ExistsAsync(filterKey))
            return false;

        // Atomic Redis SET NX with TTL
        var isNew = await redisHelper.SetIfNotExistsAsync(redisKey, filterKey,
            TimeSpan.FromDays(MyTelegramConsts.ChannelMessageViewsTtl));

        if (!isNew)
        {
            // Key already existed → record in Cuckoo for faster local lookup next time
            await cuckooFilter.AddAsync(filterKey);
            return false;
        }

        // First time view → add to local Cuckoo
        await cuckooFilter.AddAsync(filterKey);
        return true;
    }

    public void IncrementViews(long selfUserId, long channelId, int messageId)
    {
        Span<byte> key = stackalloc byte[20];
        GenerateFilterKey(selfUserId, channelId, messageId, key);
        if (CanIncrementViews(key))
        {
            _currentFilter.Add(key);
        }
    }

    public Task IncrementViewsIfNotIncrementedAsync(long selfUserId,
                    long authKeyId,
        long channelId,
        int messageId)
    {
        IncrementViews(selfUserId, channelId, messageId);

        return Task.CompletedTask;
    }

    public void LoadViewsFilters()
    {
        LoadViewsFilterDataFromLocalCachedFile(1, Filter1);
        LoadViewsFilterDataFromLocalCachedFile(2, Filter2);
        LoadViewsFilterDataFromLocalCachedFile(3, Filter3);
    }

    public void RotateDaily()
    {
        var next = (_currentIndex + 1) % 3;
        _currentIndex = next;
        switch (next)
        {
            case 0:
                Filter1.Clear();
                _currentFilter = Filter1;
                break;

            case 1:
                Filter2.Clear();
                _currentFilter = Filter2;
                break;

            case 2:
                Filter3.Clear();
                _currentFilter = Filter3;
                break;
        }

        logger.LogInformation("The channel views count filter has been rotated to filter {Index}", _currentIndex + 1);
    }

    public void SaveViewsFilters()
    {
        try
        {
            var viewsFileName1 = Path.Combine(AppContext.BaseDirectory, $"{ViewsFilterFileName}_1");
            var viewsFileName2 = Path.Combine(AppContext.BaseDirectory, $"{ViewsFilterFileName}_2");
            var viewsFileName3 = Path.Combine(AppContext.BaseDirectory, $"{ViewsFilterFileName}_3");

            File.WriteAllBytes(viewsFileName1, Filter1.GetData());
            File.WriteAllBytes(viewsFileName2, Filter2.GetData());
            File.WriteAllBytes(viewsFileName3, Filter3.GetData());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Save views filter failed");
        }
    }

    private bool CanIncrementViews(Span<byte> key)
    {
        var isExists1 = Filter1.Contains(key);
        var isExists2 = Filter2.Contains(key);
        var isExists3 = Filter3.Contains(key);

        var isExists = isExists1 || isExists2 || isExists3;

        return !isExists;
    }

    private void LoadViewsFilterDataFromLocalCachedFile(int index, SimpleCuckooFilter filter)
    {
        try
        {
            var viewsFileName = Path.Combine(AppContext.BaseDirectory, $"{ViewsFilterFileName}_{index}");
            if (File.Exists(viewsFileName))
            {
                var filterData = File.ReadAllBytes(viewsFileName);
                var isLoaded = filter.LoadData(filterData);
                if (isLoaded)
                {
                    logger.LogInformation("Channel message views filter {Index} loaded successfully, bytes: {Length}",
                        index,
                        filterData.Length);
                }
                else
                {
                    logger.LogWarning(
                        "Channel message views filter {Index} data failed to load, the filter's configuration parameters may have changed.",
                        index);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Load views filter data failed");
        }
    }
}