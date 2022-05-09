namespace MyTelegram.QueryHandlers.MongoDB.Draft;

// ReSharper disable once UnusedMember.Global
public class GetAllDraftQueryHandler : IQueryHandler<GetAllDraftQuery, IReadOnlyCollection<IDraftReadModel>>
{
    private readonly IMongoDbReadModelStore<DraftReadModel> _store;

    public GetAllDraftQueryHandler(IMongoDbReadModelStore<DraftReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IDraftReadModel>> ExecuteQueryAsync(GetAllDraftQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.OwnerPeerId == query.OwnerPeerId, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        return await cursor.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}
