namespace MyTelegram.QueryHandlers.MongoDB.PeerSettings;

public class GetPeerSettingsQueryHandler : IQueryHandler<GetPeerSettingsQuery, IPeerSettingsReadModel?>
{
    private readonly IMyMongoDbReadModelStore<PeerSettingsReadModel> _store;

    public GetPeerSettingsQueryHandler(IMyMongoDbReadModelStore<PeerSettingsReadModel> store)
    {
        _store = store;
    }

    public async Task<IPeerSettingsReadModel?> ExecuteQueryAsync(GetPeerSettingsQuery query, CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.OwnerPeerId == query.SelfUserId && p.PeerId == query.PeerId, cancellationToken: cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }
}