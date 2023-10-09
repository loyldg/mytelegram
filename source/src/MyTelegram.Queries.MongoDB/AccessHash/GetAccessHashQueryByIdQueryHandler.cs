namespace MyTelegram.QueryHandlers.MongoDB.AccessHash;

internal sealed class GetAccessHashQueryByIdQueryHandler : IQueryHandler<GetAccessHashQueryByIdQuery, IAccessHashReadModel?>
{
    private readonly IMyMongoDbReadModelStore<AccessHashReadModel> _store;

    public GetAccessHashQueryByIdQueryHandler(IMyMongoDbReadModelStore<AccessHashReadModel> store)
    {
        _store = store;
    }

    public async Task<IAccessHashReadModel?> ExecuteQueryAsync(GetAccessHashQueryByIdQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.AccessId == query.Id, cancellationToken: cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }
}