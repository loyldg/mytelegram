namespace MyTelegram.Messenger.Services.Filters;

public class InMemoryFilterDataLoader : IInMemoryFilterDataLoader
{
    //private readonly IBloomFilter _bloomFilter;
    private readonly ICuckooFilter _cuckooFilter;
    private readonly IQueryProcessor _queryProcessor;
    private readonly int _pageSize = 1000;
    private readonly ILogger<InMemoryFilterDataLoader> _logger;
    public InMemoryFilterDataLoader(//IBloomFilter bloomFilter,
        ICuckooFilter cuckooFilter,
        IQueryProcessor queryProcessor,
        ILogger<InMemoryFilterDataLoader> logger)
    {
        //_bloomFilter = bloomFilter;
        _cuckooFilter = cuckooFilter;
        _queryProcessor = queryProcessor;
        _logger = logger;
    }

    public async Task LoadAllFilterDataAsync()
    {
        // UserName data
        var hasMoreData = true;
        var skip = 0;
        var count = 0;

        while (hasMoreData)
        {
            var userNameList = await _queryProcessor.ProcessAsync(new GetAllUserNameQuery(skip, _pageSize), default)
         ;
            hasMoreData = userNameList.Count == _pageSize;
            count += userNameList.Count;
            foreach (var userName in userNameList)
            {
                await _cuckooFilter.AddAsync(Encoding.UTF8.GetBytes($"{MyTelegramServerDomainConsts.UserNameCuckooFilterKey}_{userName}"));
            }
            skip += _pageSize;
        }
        _logger.LogInformation("Load userName list ok,count={Count}", count);
    }
}