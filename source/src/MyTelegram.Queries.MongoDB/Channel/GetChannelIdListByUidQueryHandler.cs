namespace MyTelegram.QueryHandlers.MongoDB.Channel;

public class GetChannelIdListByUidQueryHandler : IQueryHandler<GetChannelIdListByUidQuery, IReadOnlyCollection<long>>
{
    private readonly IMongoDbReadModelStore<ChannelMemberReadModel> _store;

    public GetChannelIdListByUidQueryHandler(IMongoDbReadModelStore<ChannelMemberReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetChannelIdListByUidQuery query,
        CancellationToken cancellationToken)
    {
        var findOption = new FindOptions<ChannelMemberReadModel, ChannelMemberReadModel>
        {
            Projection = Builders<ChannelMemberReadModel>.Projection.Include(p => p.ChannelId)
        };

        var cursor = await _store.FindAsync(p => p.UserId == query.UserId, findOption, cancellationToken)
            ;

        var list = await cursor.ToListAsync(cancellationToken);
        return list.Select(p => p.ChannelId).ToList();
    }
}
