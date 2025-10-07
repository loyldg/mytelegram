using EventFlow.Exceptions;
using MyTelegram.Caching.Redis;
using MyTelegram.Messenger.Services.Filters;
using IMessageViews = MyTelegram.Schema.IMessageViews;
using TMessageViews = MyTelegram.Schema.TMessageViews;

namespace MyTelegram.Messenger.Services.Impl;

public class ChannelMessageViewsAppService(
    IQueryProcessor queryProcessor,
    ICommandBus commandBus,
    ICuckooFilter cuckooFilter,
    IRedisHelper redisHelper)
    : IChannelMessageViewsAppService, ITransientDependency
{
    public async Task IncrementViewsIfNotIncrementedAsync(long selfUserId,
        long channelId,
        int messageId)
    {
        var keyInfo = CreateFilterKey(selfUserId, channelId, messageId);
        await TryRegisterViewAsync(keyInfo.RedisKey, keyInfo.FilterKey);
    }

    private (string RedisKey, byte[] FilterKey) CreateFilterKey(long selfUserId,
        long channelId,
        int messageId)
    {
        var filterKey =
            $"{MyTelegramConsts.ChannelMessageViewsBloomFilterKey}_{selfUserId}_{channelId}_{messageId}";

        var redisKey =
            $"view:{selfUserId}:{channelId}:{messageId}";

        return (redisKey, Encoding.UTF8.GetBytes(filterKey));
    }

    public async Task<IList<IMessageViews>> GetMessageViewsAsync(
        long selfUserId,
        long channelId,
        List<int> messageIdList,
        bool increment = false)
    {
        if (messageIdList.Count == 0) return new List<IMessageViews>();

        var ids = messageIdList.Where(p => p > 0).ToList();
        if (ids.Count == 0)
            return messageIdList.Select(_ => (IMessageViews)new TMessageViews { Views = 0 }).ToList();

        // 1) Dedup + collect to increment
        HashSet<int> needInc = [];
        if (increment)
        {
            var pairs = ids.Select(m => (MessageId: m, Key: CreateFilterKey(selfUserId, channelId, m))).ToList();

            // batch calls (parallel awaits instead of sequential)
            var tasks = pairs.Select(async p =>
            {
                var firstTime = await TryRegisterViewAsync(p.Key.RedisKey, p.Key.FilterKey).ConfigureAwait(false);
                if (firstTime) needInc.Add(p.MessageId);
            });
            await Task.WhenAll(tasks).ConfigureAwait(false);
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
}