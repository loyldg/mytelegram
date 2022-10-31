namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

public class
    GetMessageIdListByChannelIdQueryHandler : IQueryHandler<GetMessageIdListByChannelIdQuery, IReadOnlyCollection<int>>
{
    private readonly IMyMongoDbReadModelStore<MessageReadModel> _store;

    public GetMessageIdListByChannelIdQueryHandler(IMyMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<int>> ExecuteQueryAsync(GetMessageIdListByChannelIdQuery query,
        CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<MessageReadModel, int>
        {
            Projection = new ProjectionDefinitionBuilder<MessageReadModel>().Expression(p => p.MessageId),
            Limit = query.Limit
        };

        var cursor = await _store.FindAsync(p => p.OwnerPeerId == query.ChannelId, findOptions, cancellationToken)
            .ConfigureAwait(false);
        return await cursor.ToListAsync(cancellationToken);
    }
}