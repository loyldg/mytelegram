namespace MyTelegram.QueryHandlers.MongoDB.Device;

// ReSharper disable once UnusedMember.Global
public class GetDeviceByHashQueryHandler : IQueryHandler<GetDeviceByHashQuery, IDeviceReadModel?>
{
    private readonly IMongoDbReadModelStore<DeviceReadModel> _store;

    public GetDeviceByHashQueryHandler(IMongoDbReadModelStore<DeviceReadModel> store)
    {
        _store = store;
    }

    public async Task<IDeviceReadModel?> ExecuteQueryAsync(GetDeviceByHashQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store
                .FindAsync(p => p.UserId == query.UserId && p.Hash == query.Hash, cancellationToken: cancellationToken)
            ;
        return await cursor.SingleOrDefaultAsync(cancellationToken);
    }
}
