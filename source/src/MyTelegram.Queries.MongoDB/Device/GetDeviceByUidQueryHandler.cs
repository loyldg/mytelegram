namespace MyTelegram.QueryHandlers.MongoDB.Device;

// ReSharper disable once UnusedMember.Global
public class GetDeviceByUidQueryHandler : IQueryHandler<GetDeviceByUidQuery, IReadOnlyCollection<IDeviceReadModel>>
{
    private readonly IMongoDbReadModelStore<DeviceReadModel> _store;

    public GetDeviceByUidQueryHandler(IMongoDbReadModelStore<DeviceReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IDeviceReadModel>> ExecuteQueryAsync(GetDeviceByUidQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.UserId == query.UserId && p.IsActive, cancellationToken: cancellationToken)
            ;
        return await cursor.ToListAsync(cancellationToken);
    }
}
