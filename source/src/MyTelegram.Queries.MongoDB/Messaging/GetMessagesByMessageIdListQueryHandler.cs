namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

// ReSharper disable once UnusedMember.Global
public class GetMessagesByMessageIdListQueryHandler : IQueryHandler<GetMessagesByMessageIdListQuery,
    IReadOnlyCollection<IMessageReadModel>>
{
    private readonly IMongoDbReadModelStore<MessageReadModel> _store;

    public GetMessagesByMessageIdListQueryHandler(IMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IMessageReadModel>> ExecuteQueryAsync(
        GetMessagesByMessageIdListQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store
            .FindAsync(p => query.MessageIdList.Contains(p.MessageId), cancellationToken: cancellationToken)
            ;
        return await cursor.ToListAsync(cancellationToken);
    }
}
