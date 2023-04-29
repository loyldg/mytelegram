namespace MyTelegram.QueryHandlers.MongoDB.PeerNotifySettings;

// ReSharper disable once UnusedMember.Global
public class GetPeerNotifySettingsListQueryHandler : IQueryHandler<GetPeerNotifySettingsListQuery,
    IReadOnlyCollection<IPeerNotifySettingsReadModel>>
{
    private readonly IMongoDbReadModelStore<PeerNotifySettingsReadModel> _store;

    public GetPeerNotifySettingsListQueryHandler(IMongoDbReadModelStore<PeerNotifySettingsReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IPeerNotifySettingsReadModel>> ExecuteQueryAsync(
        GetPeerNotifySettingsListQuery query,
        CancellationToken cancellationToken)
    {
        var peerIdList = query.PeerNotifySettingsIdList.Select(p => p.Value).ToList();
        var cursor = await _store.FindAsync(p => peerIdList.Contains(p.Id), cancellationToken: cancellationToken)
            ;

        return await cursor.ToListAsync(cancellationToken);
    }
}
