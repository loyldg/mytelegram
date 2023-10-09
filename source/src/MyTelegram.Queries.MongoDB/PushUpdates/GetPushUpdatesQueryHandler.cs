//namespace MyTelegram.QueryHandlers.MongoDB.PushUpdates;

//// ReSharper disable once UnusedMember.Global
//public class GetPushUpdatesQueryHandler : IQueryHandler<GetPushUpdatesQuery, IReadOnlyCollection<IPushUpdatesReadModel>>
//{
//    private readonly IMongoDbReadModelStore<PushUpdatesReadModel> _store;

//    public GetPushUpdatesQueryHandler(IMongoDbReadModelStore<PushUpdatesReadModel> store)
//    {
//        _store = store;
//    }

//    public async Task<IReadOnlyCollection<IPushUpdatesReadModel>> ExecuteQueryAsync(GetPushUpdatesQuery query,
//        CancellationToken cancellationToken)
//    {
//        var options = new FindOptions<PushUpdatesReadModel, PushUpdatesReadModel> { Limit = query.Limit, Skip = 0 };

//        var cursor = await _store.FindAsync(p => p.PeerId == query.PeerId && p.Pts > query.MinPts,
//            options,
//            cancellationToken);
//        return await cursor.ToListAsync(cancellationToken);
//    }
//}
