using MyTelegram.ReadModel;

namespace MyTelegram.QueryHandlers.MongoDB.Updates;

public class GetUpdatesQueryHandler : IQueryHandler<GetUpdatesQuery, IReadOnlyCollection<IUpdatesReadModel>>
{
    private readonly IMongoDbReadModelStore<UpdatesReadModel> _store;

    public GetUpdatesQueryHandler(IMongoDbReadModelStore<UpdatesReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IUpdatesReadModel>> ExecuteQueryAsync(GetUpdatesQuery query,
        CancellationToken cancellationToken)
    {
        Expression<Func<UpdatesReadModel, bool>> predicate = p => (p.OwnerPeerId == query.PeerId) /*&& (p.OnlySendToUserId == null || p.OnlySendToUserId == query.SelfUserId) */&& p.Pts > query.MinPts;
        if (query.Date > 0)
        {
            predicate = predicate.And(p => p.Date > query.Date);
        }

        var options = new FindOptions<UpdatesReadModel, UpdatesReadModel> { Limit = query.Limit, Skip = 0 };

        var cursor = await _store.FindAsync(predicate,
            options,
            cancellationToken);
        return await cursor.ToListAsync(cancellationToken);
    }
}

public class GetChannelUpdatesByGlobalSeqNoQueryHandler : IQueryHandler<GetChannelUpdatesByGlobalSeqNoQuery,
    IReadOnlyCollection<IUpdatesReadModel>>
{
    private readonly IMongoDbReadModelStore<UpdatesReadModel> _store;

    public GetChannelUpdatesByGlobalSeqNoQueryHandler(IMongoDbReadModelStore<UpdatesReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IUpdatesReadModel>> ExecuteQueryAsync(GetChannelUpdatesByGlobalSeqNoQuery query, CancellationToken cancellationToken)
    {
        var options = new FindOptions<UpdatesReadModel, UpdatesReadModel> { Limit = query.Limit, Skip = 0 };
        var cursor = await _store.FindAsync(p =>
            query.ChannelIdList.Contains(p.OwnerPeerId) && p.GlobalSeqNo > query.MinGlobalSeqNo, options, cancellationToken: cancellationToken);

        return await cursor.ToListAsync(cancellationToken: cancellationToken);
    }
}