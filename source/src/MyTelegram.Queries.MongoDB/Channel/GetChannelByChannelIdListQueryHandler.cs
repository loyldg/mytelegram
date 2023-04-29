namespace MyTelegram.QueryHandlers.MongoDB.Channel;

public class
    GetChannelByChannelIdListQueryHandler : IQueryHandler<GetChannelByChannelIdListQuery,
        IReadOnlyCollection<IChannelReadModel>>
{
    private readonly IMongoDbReadModelStore<ChannelReadModel> _store;

    public GetChannelByChannelIdListQueryHandler(IMongoDbReadModelStore<ChannelReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IChannelReadModel>> ExecuteQueryAsync(GetChannelByChannelIdListQuery query,
        CancellationToken cancellationToken)
    {
        if (query.ChannelIdList.Count == 0)
        {
            return new List<ChannelReadModel>();
        }

        // todo:pagination
        var cursor = await _store
            .FindAsync(p => query.ChannelIdList.Contains(p.ChannelId), cancellationToken: cancellationToken)
            ;
        return await cursor.ToListAsync(cancellationToken);
    }
}
