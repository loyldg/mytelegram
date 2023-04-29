namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

public class
    GetMessageIdListByUserIdQueryHandler : IQueryHandler<GetMessageIdListByUserIdQuery, IReadOnlyCollection<int>>
{
    private readonly IMyMongoDbReadModelStore<MessageReadModel> _store;

    public GetMessageIdListByUserIdQueryHandler(IMyMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<int>> ExecuteQueryAsync(GetMessageIdListByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<MessageReadModel, int>
        {
            Projection = new ProjectionDefinitionBuilder<MessageReadModel>().Expression(p => p.MessageId),
            Limit = query.Limit
        };

        Expression<Func<MessageReadModel, bool>> predicate = x => x.OwnerPeerId == query.ChannelId;
        predicate = predicate.WhereIf(query.SenderUserId != 0, p => p.SenderPeerId == query.SenderUserId);

        var cursor = await _store.FindAsync(predicate,
            findOptions,
            cancellationToken);

        return await cursor.ToListAsync(cancellationToken);
    }
}
