namespace MyTelegram.QueryHandlers.MongoDB.Device;

// ReSharper disable once UnusedMember.Global
public class GetDeviceByAuthKeyIdQueryHandler : IQueryHandler<GetDeviceByAuthKeyIdQuery, IDeviceReadModel?>
{
    private readonly IMongoDbReadModelStore<DeviceReadModel> _store;

    public GetDeviceByAuthKeyIdQueryHandler(IMongoDbReadModelStore<DeviceReadModel> store)
    {
        _store = store;
    }

    public async Task<IDeviceReadModel?> ExecuteQueryAsync(GetDeviceByAuthKeyIdQuery query,
        CancellationToken cancellationToken)
    {
        var item = await _store.GetAsync(DeviceId.Create(query.AuthKeyId).Value, cancellationToken)
            ;

        return item.ReadModel;
    }
}
