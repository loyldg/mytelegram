namespace MyTelegram.QueryHandlers.MongoDB.Dialog;

// ReSharper disable once UnusedMember.Global
public class GetDialogsQueryHandler : IQueryHandler<GetDialogsQuery, IReadOnlyList<IDialogReadModel>>
{
    private readonly IMongoDbReadModelStore<DialogReadModel> _store;

    public GetDialogsQueryHandler(IMongoDbReadModelStore<DialogReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyList<IDialogReadModel>> ExecuteQueryAsync(GetDialogsQuery query,
        CancellationToken cancellationToken)
    {
        // Fix native aot mission metadata issues
        var needPinnedParameter = false;
        var needOffsetDate = false;
        var pinned = false;
        var offsetDate = DateTime.UtcNow;
        if (query.Pinned.HasValue)
        {
            needPinnedParameter = true;
            pinned = query.Pinned.Value;
        }

        if (query.OffsetDate.HasValue)
        {
            needOffsetDate = true;
            offsetDate = query.OffsetDate.Value;
        }

        Expression<Func<DialogReadModel, bool>> predicate = x => x.OwnerId == query.OwnerId;
        predicate = predicate
                .WhereIf(needOffsetDate, p => p.CreationTime < offsetDate)
                .WhereIf(needPinnedParameter, p => p.Pinned == pinned)
                .WhereIf(query.PeerIdList?.Count > 0, p => query.PeerIdList!.Contains(p.ToPeerId))
            ;
        var options = new FindOptions<DialogReadModel, DialogReadModel>
        {
            Limit = query.Limit,
            Skip = 0,
            Sort = Builders<DialogReadModel>.Sort.Descending(p => p.TopMessage)
        };
        var cursor = await _store.FindAsync(predicate, options, cancellationToken);
        return await cursor.ToListAsync(cancellationToken);
    }
}
