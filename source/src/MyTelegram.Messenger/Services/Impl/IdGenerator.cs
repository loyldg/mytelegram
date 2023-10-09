namespace MyTelegram.Messenger.Services.Impl;

public class IdGenerator : IIdGenerator
{
    private readonly IHiLoValueGeneratorCache _cache;
    private readonly IHiLoValueGeneratorFactory _factory;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IHiLoStateBlockSizeHelper _stateBlockSizeHelper;
    private readonly ILogger<IdGenerator> _logger;
    public IdGenerator(IHiLoValueGeneratorCache cache,
        IHiLoValueGeneratorFactory factory, IPeerHelper peerHelper, IQueryProcessor queryProcessor, IHiLoStateBlockSizeHelper stateBlockSizeHelper, ILogger<IdGenerator> logger)
    {
        _cache = cache;
        _factory = factory;
        _peerHelper = peerHelper;
        _queryProcessor = queryProcessor;
        _stateBlockSizeHelper = stateBlockSizeHelper;
        _logger = logger;
    }

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
        //var state = _cache.GetOrAdd(idType, id);
        var sw = Stopwatch.StartNew();
        HiLoValueGeneratorState state;
        if (_peerHelper.IsChannelPeer(id) && idType == IdType.MessageId)
        //if (idType == IdType.ChannelId)
        {
            state = await _cache.GetOrAddAsync(idType, id, async () =>
            {
                var blockSize = _stateBlockSizeHelper.GetBlockSize(idType);

                var channelReadModel = await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(id), cancellationToken);
                //var channelMaxMessageId =
                //    await _queryProcessor.ProcessAsync(new GetChannelMaxMessageIdQuery(id), cancellationToken);
                var channelMaxMessageId = channelReadModel!.TopMessageId;
                var high = channelMaxMessageId / blockSize;
                var low = channelMaxMessageId % blockSize;
                if (low != 0)
                {
                    high++;
                }
                //// 100 ,990  ->low 990%100=9 high=9
                //// 10  ,990  ->low 990%10 =0 high=99

                //Console.WriteLine($"###Get channel message id from database,channelId={id},maxId={channelMaxMessageId},high={high * blockSize},low={low}");

                return new HiLoValueGeneratorState(blockSize, channelMaxMessageId, high * blockSize + 1);

            });

            //state = _cache.GetOrAdd(idType, id);
        }
        else
        {
            state = _cache.GetOrAdd(idType, id);
        }

        var generator = _factory.Create(state);
        var nextId = await generator.NextAsync(idType, id, cancellationToken);
        sw.Stop();

        if (sw.Elapsed.TotalMilliseconds > 100)
        {
            _logger.LogWarning("[{Timespan}]Generate id to slow,idType={IdType},id={Id}", sw.Elapsed, idType, id);
        }

        return nextId + GetInitId(idType);
    }

    private static long GetInitId(IdType idType)
    {
        return idType switch
        {
            IdType.ChannelId => MyTelegramServerDomainConsts.ChannelInitId,
            IdType.UserId => MyTelegramServerDomainConsts.UserIdInitId + 10000, //前10000个用户为测试用户
            IdType.BotUserId => MyTelegramServerDomainConsts.BotUserInitId,
            IdType.ChatId => MyTelegramServerDomainConsts.ChatIdInitId,
            _ => 0
        };
    }
}