using EventFlow.Exceptions;
using MyTelegram.Messenger.Services.Filters;
using IMessageViews = MyTelegram.Schema.IMessageViews;

namespace MyTelegram.Messenger.Services.Impl;

public class ChannelMessageViewsAppService(
    IQueryProcessor queryProcessor,
    ICommandBus commandBus,
    ICuckooFilter cuckooFilter)
    : IChannelMessageViewsAppService, ITransientDependency
{
    public async Task IncrementViewsIfNotIncrementedAsync(long selfUserId,
        long channelId,
        int messageId)
    {
        var key = GetFilterKey(selfUserId, channelId, messageId);
        var isExists = await cuckooFilter.ExistsAsync(key);
        if (!isExists)
        {
            await cuckooFilter.AddAsync(key);
        }
    }

    private byte[] GetFilterKey(long selfUserId,
        long channelId,
        int messageId) =>
        Encoding.UTF8.GetBytes(
            $"{MyTelegramConsts.ChannelMessageViewsBloomFilterKey}_{selfUserId}_{channelId}_{messageId}");

    public async Task<IList<IMessageViews>> GetMessageViewsAsync(long selfUserId,
        long channelId,
        List<int> messageIdList)
    {
        var messageIdGreaterThanZeroList = messageIdList.Where(p => p > 0).ToList();
        var keyList = messageIdGreaterThanZeroList
            .Select(p => GetFilterKey(selfUserId, channelId, p)).ToList();

        var needIncrementMessageIdList = new List<int>();
        var index = 0;

        foreach (var key in keyList)
        {
            var isExists = await cuckooFilter.ExistsAsync(key);
            if (!isExists)
            {
                await cuckooFilter.AddAsync(key);
                needIncrementMessageIdList.Add(messageIdGreaterThanZeroList[index]);
            }
            index++;
        }

        var messageViews = (await queryProcessor
                    .ProcessAsync(new GetMessageViewsQuery(channelId, messageIdGreaterThanZeroList), default)
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
            catch (DomainError)
            {
                //
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
}