namespace MyTelegram.QueryHandlers.MongoDB.Channel;

// ReSharper disable once UnusedMember.Global
public class
    GetMegaGroupByUidQueryHandler : IQueryHandler<GetMegaGroupByUidQuery, IReadOnlyCollection<IChannelReadModel>>
{
    private readonly IMongoDbReadModelStore<ChannelReadModel> _store;

    public GetMegaGroupByUidQueryHandler(IMongoDbReadModelStore<ChannelReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IChannelReadModel>> ExecuteQueryAsync(GetMegaGroupByUidQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store
            .FindAsync(p => p.MegaGroup && p.CreatorId == query.UserId, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        return await cursor.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}
