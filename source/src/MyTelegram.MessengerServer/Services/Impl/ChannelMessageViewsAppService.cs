using IMessageViews = MyTelegram.Schema.IMessageViews;

namespace MyTelegram.MessengerServer.Services.Impl;

public class ChannelMessageViewsAppService : IChannelMessageViewsAppService //, ISingletonDependency
{
    //private readonly IBloomFilter _bloomFilter;
    private readonly ICommandBus _commandBus;
    private readonly IQueryProcessor _queryProcessor;
    private readonly ICuckooFilter _cuckooFilter;
    public ChannelMessageViewsAppService(
        IQueryProcessor queryProcessor,
        ICommandBus commandBus,
        ICuckooFilter cuckooFilter)
    {
        _queryProcessor = queryProcessor;
        _commandBus = commandBus;
        _cuckooFilter = cuckooFilter;
    }

    public async Task IncrementViewsIfNotIncrementedAsync(long selfUserId,
        long authKeyId,
        long channelId,
        int messageId)
    {
        var key = GetFilterKey(selfUserId, authKeyId, channelId, messageId);
        var isExists = await _cuckooFilter.ExistsAsync(key).ConfigureAwait(false);
        if (!isExists)
        {
            await _cuckooFilter.AddAsync(key).ConfigureAwait(false);
        }
    }

    private byte[] GetFilterKey(long selfUserId,
        long authKeyId,
        long channelId,
        int messageId) =>
        Encoding.UTF8.GetBytes(
            $"{MyTelegramServerDomainConsts.ChannelMessageViewsBloomFilterKey}_{selfUserId}_{authKeyId}_{channelId}_{messageId}");

    public async Task<IList<IMessageViews>> GetMessageViewsAsync(long selfUserId,
        long authKeyId,
        long channelId,
        List<int> messageIdList)
    {
        var messageIdGreaterThanZeroList = messageIdList.Where(p => p > 0).ToList();
        var keyList = messageIdGreaterThanZeroList
            .Select(p => GetFilterKey(selfUserId, authKeyId, channelId, p)).ToList();



        //var resultList = await _bloomFilter
        //    .ExistsAsync(keyList).ConfigureAwait(false);


        //var needAddToList = new List<byte[]>();
        var needIncrementMessageIdList = new List<int>();
        var index = 0;

        foreach (var key in keyList)
        {
            var isExists = await _cuckooFilter.ExistsAsync(key).ConfigureAwait(false);
            if (!isExists)
            {
                await _cuckooFilter.AddAsync(key).ConfigureAwait(false);
                needIncrementMessageIdList.Add(messageIdGreaterThanZeroList[index]);
            }
            index++;
        }

        //foreach (var isExists in resultList)
        //{
        //    if (!isExists)
        //    {
        //        needAddToList.Add(keyList[index]);
        //        needIncrementMessageIdList.Add(messageIdGreaterThanZeroList[index]);
        //    }

        //    index++;
        //}

        //if (needAddToList.Count > 0)
        //{
        //    await _bloomFilter.AddAsync(needAddToList).ConfigureAwait(false);
        //}

        var messageViews = (await _queryProcessor
                    .ProcessAsync(new GetMessageViewsQuery(channelId, messageIdGreaterThanZeroList), default)
                    .ConfigureAwait(false))
                .ToDictionary(k => k.MessageId, v => v)
            ;

        foreach (var messageId in needIncrementMessageIdList)
        {
            try
            {
                var command = new IncrementViewsCommand(MessageId.Create(channelId, messageId));
                await _commandBus.PublishAsync(command, default).ConfigureAwait(false);
            }
            catch (DomainError)
            {
                //
            }
        }

        var messageViewsToClient = new List<IMessageViews>();
        foreach (var messageId in messageIdList)
        {
            var needIncrement = needIncrementMessageIdList.Contains(messageId);
            if (messageViews.TryGetValue(messageId, out var views))
            {
                messageViewsToClient.Add(new Schema.TMessageViews
                {
                    Views = needIncrement ? views.Views + 1 : views.Views,
                    Replies = new TMessageReplies { ChannelId = channelId }
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