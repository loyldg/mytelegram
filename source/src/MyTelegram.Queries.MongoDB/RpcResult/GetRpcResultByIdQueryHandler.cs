namespace MyTelegram.QueryHandlers.MongoDB.RpcResult;

// ReSharper disable once UnusedMember.Global
public class GetRpcResultByIdQueryHandler : IQueryHandler<GetRpcResultByIdQuery, IRpcResultReadModel?>
{
    private readonly IMongoDbReadModelStore<RpcResultReadModel> _store;

    public GetRpcResultByIdQueryHandler(IMongoDbReadModelStore<RpcResultReadModel> store)
    {
        _store = store;
    }

    public async Task<IRpcResultReadModel?> ExecuteQueryAsync(GetRpcResultByIdQuery query,
        CancellationToken cancellationToken)
    {
        var item = await _store.GetAsync(query.Id, cancellationToken).ConfigureAwait(false);
        return item.ReadModel;
    }
}
