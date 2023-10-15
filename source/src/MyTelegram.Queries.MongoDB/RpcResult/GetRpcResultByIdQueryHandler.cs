namespace MyTelegram.QueryHandlers.MongoDB.RpcResult;

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
        var item = await _store.GetAsync(query.Id, cancellationToken);
        return item.ReadModel;
    }
}

public class GetRpcResultQueryHandler : IQueryHandler<GetRpcResultQuery, IRpcResultReadModel?>
{
    private readonly IMongoDbReadModelStore<RpcResultReadModel> _store;

    public GetRpcResultQueryHandler(IMongoDbReadModelStore<RpcResultReadModel> store)
    {
        _store = store;
    }

    public async Task<IRpcResultReadModel?> ExecuteQueryAsync(GetRpcResultQuery query, CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.PeerId == query.UserId && p.ReqMsgId == query.ReqMsgId, cancellationToken: cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}