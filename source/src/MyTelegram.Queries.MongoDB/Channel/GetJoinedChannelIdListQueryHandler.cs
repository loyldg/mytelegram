namespace MyTelegram.QueryHandlers.MongoDB.Channel;

// ReSharper disable once UnusedMember.Global
public class
    GetJoinedChannelIdListQueryHandler : IQueryHandler<GetJoinedChannelIdListQuery, IReadOnlyCollection<long>>
{
    private readonly IMyMongoDbReadModelStore<ChannelMemberReadModel> _store;

    public GetJoinedChannelIdListQueryHandler(IMyMongoDbReadModelStore<ChannelMemberReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetJoinedChannelIdListQuery query,
        CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<ChannelMemberReadModel, long>
        {
            Projection = new ProjectionDefinitionBuilder<ChannelMemberReadModel>().Expression(p => p.ChannelId)
        };
        var cursor = await _store
            .FindAsync(p => p.UserId == query.MemberUid && query.ChannelIdList.Contains(p.ChannelId),
                findOptions,
                cancellationToken);
        return await cursor.ToListAsync(cancellationToken);
    }
}
