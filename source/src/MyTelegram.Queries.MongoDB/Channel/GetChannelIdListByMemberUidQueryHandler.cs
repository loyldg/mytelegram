namespace MyTelegram.QueryHandlers.MongoDB.Channel;

public class
    GetChannelIdListByMemberUidQueryHandler : IQueryHandler<GetChannelIdListByMemberUidQuery,
        IReadOnlyCollection<long>>
{
    private readonly IMyMongoDbReadModelStore<ChannelMemberReadModel> _store;

    public GetChannelIdListByMemberUidQueryHandler(IMyMongoDbReadModelStore<ChannelMemberReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetChannelIdListByMemberUidQuery query,
        CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<ChannelMemberReadModel, long> {
            Projection = new ProjectionDefinitionBuilder<ChannelMemberReadModel>().Expression(p => p.ChannelId),
            Limit = 100
        };
        var cursor = await _store.FindAsync(p => p.UserId == query.MemberUid, findOptions, cancellationToken)
            ;
        return await cursor.ToListAsync(cancellationToken);
    }
}
