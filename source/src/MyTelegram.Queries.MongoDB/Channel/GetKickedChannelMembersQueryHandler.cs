namespace MyTelegram.QueryHandlers.MongoDB.Channel;

// ReSharper disable once UnusedMember.Global
public class GetKickedChannelMembersQueryHandler : IQueryHandler<GetKickedChannelMembersQuery,
    IReadOnlyCollection<IChannelMemberReadModel>>
{
    private readonly IMongoDbReadModelStore<ChannelMemberReadModel> _store;

    public GetKickedChannelMembersQueryHandler(IMongoDbReadModelStore<ChannelMemberReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IChannelMemberReadModel>> ExecuteQueryAsync(
        GetKickedChannelMembersQuery query,
        CancellationToken cancellationToken)
    {
        var options = new FindOptions<ChannelMemberReadModel, ChannelMemberReadModel>
        {
            Limit = query.Limit,
            Skip = query.Offset
        };
        var cursor = await _store.FindAsync(p => p.ChannelId == query.ChannelId && p.Kicked, options, cancellationToken)
            ;

        return await cursor.ToListAsync(cancellationToken);
    }
}
