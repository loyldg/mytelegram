namespace MyTelegram.QueryHandlers.MongoDB.Channel;

// ReSharper disable once UnusedMember.Global
public class GetChannelMemberListByChannelIdListQueryHandler : IQueryHandler<
    GetChannelMemberListByChannelIdListQuery, IReadOnlyCollection<IChannelMemberReadModel>>
{
    private readonly IMongoDbReadModelStore<ChannelMemberReadModel> _store;

    public GetChannelMemberListByChannelIdListQueryHandler(IMongoDbReadModelStore<ChannelMemberReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IChannelMemberReadModel>> ExecuteQueryAsync(
        GetChannelMemberListByChannelIdListQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store
                .FindAsync(p => p.UserId == query.MemberUserId && query.ChannelIdList.Contains(p.ChannelId),
                    cancellationToken: cancellationToken)
            ;
        return await cursor.ToListAsync(cancellationToken);
    }
}
