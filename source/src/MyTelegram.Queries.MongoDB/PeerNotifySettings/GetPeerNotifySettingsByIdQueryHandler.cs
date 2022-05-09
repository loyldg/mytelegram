namespace MyTelegram.QueryHandlers.MongoDB.PeerNotifySettings;

// ReSharper disable once UnusedMember.Global
public class
    GetPeerNotifySettingsByIdQueryHandler : IQueryHandler<GetPeerNotifySettingsByIdQuery,
        IPeerNotifySettingsReadModel>
{
    private readonly IMongoDbReadModelStore<PeerNotifySettingsReadModel> _store;

    public GetPeerNotifySettingsByIdQueryHandler(IMongoDbReadModelStore<PeerNotifySettingsReadModel> store)
    {
        _store = store;
    }

    public async Task<IPeerNotifySettingsReadModel> ExecuteQueryAsync(GetPeerNotifySettingsByIdQuery query,
        CancellationToken cancellationToken)
    {
        return (await _store.GetAsync(query.Id.Value, cancellationToken).ConfigureAwait(false)).ReadModel ??
               new PeerNotifySettingsReadModel();
    }
}
