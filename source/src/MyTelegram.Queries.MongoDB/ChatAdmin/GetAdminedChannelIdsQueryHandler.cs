namespace MyTelegram.QueryHandlers.MongoDB.ChatAdmin;

public class GetAdminedChannelIdsQueryHandler : IQueryHandler<GetAdminedChannelIdsQuery, IReadOnlyCollection<long>>
{
    private readonly IMyMongoDbReadModelStore<ChatAdminReadModel> _store;

    public GetAdminedChannelIdsQueryHandler(IMyMongoDbReadModelStore<ChatAdminReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetAdminedChannelIdsQuery query, CancellationToken cancellationToken)
    {
        var findOption = new FindOptions<ChatAdminReadModel, long>
        {
            Projection = Builders<ChatAdminReadModel>.Projection.Expression(p => p.PeerId),
        };

        var cursor = await _store.FindAsync(p => p.UserId == query.UserId, findOption, cancellationToken);

        return await cursor.ToListAsync(cancellationToken: cancellationToken);
    }
}