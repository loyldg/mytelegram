using IMessageViews=MyTelegram.Schema.IMessageViews;
using TMessageViews=MyTelegram.Schema.TMessageViews;

namespace MyTelegram.MessengerServer.Services.Impl;

public class ChannelMessageViewsAppService : IChannelMessageViewsAppService //, ISingletonDependency
{
    private readonly IBloomFilter _bloomFilter;
    private readonly ICommandBus _commandBus;
    private readonly IQueryProcessor _queryProcessor;

    public ChannelMessageViewsAppService(IBloomFilter bloomFilter,
        IQueryProcessor queryProcessor,
        ICommandBus commandBus)
    {
        _bloomFilter = bloomFilter;
        _queryProcessor = queryProcessor;
        _commandBus = commandBus;
    }

    public async Task<IList<IMessageViews>> GetMessageViewsAsync(long selfUserId,
        long authKeyId,
        long channelId,
        List<int> messageIdList)
    {
        var messageIdGreaterThanZeroList = messageIdList.Where(p => p > 0).ToList();
        var keyList = messageIdGreaterThanZeroList
            .Select(p => Encoding.UTF8.GetBytes($"{MyTelegramServerDomainConsts.ChannelMessageViewsBloomFilterKey}_{selfUserId}_{authKeyId}_{channelId}_{p}")).ToList();
        var resultList = await _bloomFilter
            .ExistsAsync(keyList).ConfigureAwait(false);
        var needAddToList = new List<byte[]>();
        var needIncrementMessageIdList = new List<int>();
        var index = 0;
        foreach (var isExists in resultList)
        {
            if (!isExists)
            {
                needAddToList.Add(keyList[index]);
                needIncrementMessageIdList.Add(messageIdGreaterThanZeroList[index]);
            }

            index++;
        }

        if (needAddToList.Count > 0)
        {
            await _bloomFilter.AddAsync(needAddToList)
                .ConfigureAwait(false);
        }

        var messageViews = (await _queryProcessor
                    .ProcessAsync(new GetMessageViewsQuery(channelId, messageIdGreaterThanZeroList), default)
                    .ConfigureAwait(false))
                .ToDictionary(k => k.MessageId, v => v)
            ;

        foreach (var messageId in needIncrementMessageIdList)
        {
            var command = new IncrementViewsCommand(MessageId.Create(channelId, messageId));
            //Console.WriteLine(
            //    $"IncrementViewsCommand channelid={channelId} messageId={messageId} ,msgBoxId={command.AggregateId}");
            await _commandBus.PublishAsync(command, default).ConfigureAwait(false);
        }

        var messageViewsToClient = new List<IMessageViews>();
        foreach (var messageId in messageIdList)
        {
            var needIncrement = needIncrementMessageIdList.Contains(messageId);
            if (messageViews.TryGetValue(messageId, out var views))
            {
                messageViewsToClient.Add(new TMessageViews
                {
                    Views = needIncrement ? views.Views + 1 : views.Views,
                    Replies = new TMessageReplies { ChannelId = channelId }
                });
            }
            else
            {
                messageViewsToClient.Add(new TMessageViews { Views = 0 });
            }
        }

        return messageViewsToClient;
    }
}