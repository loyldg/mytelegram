using MyTelegram.Messenger.Services.Filters;
using System.Buffers.Binary;
using IMessageViews = MyTelegram.Schema.IMessageViews;

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
        List<int> messageIdList)
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

        var linkedChannelId = await queryProcessor.ProcessAsync(new GetLinkedChannelIdQuery(channelId));

        var replies = (await queryProcessor.ProcessAsync(new GetRepliesQuery(channelId, messageIdList)))
                .ToDictionary(k => k.MessageId, v => v);

        var messageViewsToClient = new List<IMessageViews>();
        foreach (var messageId in messageIdList)
        {
            var needIncrement = needIncrementMessageIdList.Contains(messageId);
            if (messageViews.TryGetValue(messageId, out var views))
            {
                replies.TryGetValue(messageId, out var reply);
                var recentRepliers = new List<IPeer>();// = reply?.RecentRepliers.Select(p => p.ToPeer());
                if (reply?.RecentRepliers?.Count > 0)
                {
                    recentRepliers.AddRange(reply.RecentRepliers.Select(peer => peer.ToPeer()));
                }

                messageViewsToClient.Add(new Schema.TMessageViews
                {
                    Views = needIncrement ? views.Views + 1 : views.Views,
                    //Replies = new TMessageReplies { ChannelId = channelId }
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
                messageViewsToClient.Add(new Schema.TMessageViews { Views = 0 });
            }
        }

        return messageViewsToClient;
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