namespace MyTelegram.QueryHandlers.MongoDB.Pts;

// ReSharper disable once UnusedMember.Global
public class GetPtsByPermAuthKeyIdQueryHandler : IQueryHandler<GetPtsByPermAuthKeyIdQuery, IPtsForAuthKeyIdReadModel?>
{
    private readonly IMongoDbReadModelStore<PtsForAuthKeyIdReadModel> _store;

    public GetPtsByPermAuthKeyIdQueryHandler(IMongoDbReadModelStore<PtsForAuthKeyIdReadModel> store)
    {
        _store = store;
    }

    public async Task<IPtsForAuthKeyIdReadModel?> ExecuteQueryAsync(GetPtsByPermAuthKeyIdQuery query,
        CancellationToken cancellationToken)
    {
        var id = PtsId.Create(query.PeerId, query.PermAuthKeyId);
        var item = await _store.FindAsync(p => p.Id == id.Value, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return await item.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
    }
}
