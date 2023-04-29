namespace MyTelegram.QueryHandlers.MongoDB.Pts;

// ReSharper disable once UnusedMember.Global
public class GetPtsByPeerIdQueryHandler : IQueryHandler<GetPtsByPeerIdQuery, IPtsReadModel?>
{
    private readonly IMongoDbReadModelStore<PtsReadModel> _store;

    public GetPtsByPeerIdQueryHandler(IMongoDbReadModelStore<PtsReadModel> store)
    {
        _store = store;
    }

    public async Task<IPtsReadModel?> ExecuteQueryAsync(GetPtsByPeerIdQuery query,
        CancellationToken cancellationToken)
    {
        var ptsId = PtsId.Create(query.PeerId).Value;
        var item = await _store.GetAsync(ptsId, cancellationToken);
        return item.ReadModel;
    }
}
