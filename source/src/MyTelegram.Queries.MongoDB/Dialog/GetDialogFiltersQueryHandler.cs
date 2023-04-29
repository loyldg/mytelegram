namespace MyTelegram.QueryHandlers.MongoDB.Dialog;

public class
    GetDialogFiltersQueryHandler : IQueryHandler<GetDialogFiltersQuery, IReadOnlyCollection<IDialogFilterReadModel>>
{
    private readonly IMongoDbReadModelStore<DialogFilterReadModel> _store;

    public GetDialogFiltersQueryHandler(IMongoDbReadModelStore<DialogFilterReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IDialogFilterReadModel>> ExecuteQueryAsync(GetDialogFiltersQuery query,
        CancellationToken cancellationToken)
    {
        var cursor =
            await _store.FindAsync(p => p.OwnerUserId == query.OwnerUserId, cancellationToken: cancellationToken);

        return await cursor.ToListAsync(cancellationToken);
    }
}
