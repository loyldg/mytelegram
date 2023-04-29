namespace MyTelegram.QueryHandlers.MongoDB.Channel;

public class GetChannelMembersByChannelIdQueryHandler : IQueryHandler<GetChannelMembersByChannelIdQuery,
    IReadOnlyCollection<IChannelMemberReadModel>>
{
    private readonly IMongoDbReadModelStore<ChannelMemberReadModel> _store;

    public GetChannelMembersByChannelIdQueryHandler(IMongoDbReadModelStore<ChannelMemberReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IChannelMemberReadModel>> ExecuteQueryAsync(
        GetChannelMembersByChannelIdQuery query,
        CancellationToken cancellationToken)
    {
        var options = new FindOptions<ChannelMemberReadModel, ChannelMemberReadModel>
        {
            Limit = query.Limit,
            Skip = query.Offset
        };

        if (query.MemberUidList.Count > 0)
        {
            var cursor = await _store
                .FindAsync(p => !p.Left && p.ChannelId == query.ChannelId && query.MemberUidList.Contains(p.UserId),
                    options,
                    cancellationToken)
                ;
            return await cursor.ToListAsync(cancellationToken);
        } else
        {
            var cursor = await _store
                .FindAsync(p => !p.Left && p.ChannelId == query.ChannelId, options, cancellationToken)
                ;
            return await cursor.ToListAsync(cancellationToken);
        }
    }
}
