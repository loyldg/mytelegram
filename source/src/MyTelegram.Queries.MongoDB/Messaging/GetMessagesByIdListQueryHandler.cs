namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

// ReSharper disable once UnusedMember.Global
public class GetMessagesByIdListQueryHandler : IQueryHandler<GetMessagesByIdListQuery,
    IReadOnlyList<IMessageReadModel>>
{
    private readonly IMongoDbReadModelStore<MessageReadModel> _store;

    public GetMessagesByIdListQueryHandler(IMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyList<IMessageReadModel>> ExecuteQueryAsync(
        GetMessagesByIdListQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store
                .FindAsync(p => query.MessageIdList.Contains(p.Id), cancellationToken: cancellationToken)
                .ConfigureAwait(false)
            ;
        return await cursor.ToListAsync(cancellationToken);
    }
}
