namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

// ReSharper disable once UnusedMember.Global
public class GetMessageIdListQueryHandler : IQueryHandler<GetMessageIdListQuery, List<int>>
{
    private readonly IMyMongoDbReadModelStore<MessageReadModel> _store;

    public GetMessageIdListQueryHandler(IMyMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<List<int>> ExecuteQueryAsync(GetMessageIdListQuery query,
        CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<MessageReadModel, int> {
            Projection = new ProjectionDefinitionBuilder<MessageReadModel>().Expression(p => p.MessageId),
            Limit = query.Limit
        };
        var maxId = query.MaxMessageId;
        if (maxId == 0)
        {
            maxId = int.MaxValue;
        }

        var cursor = await _store
            .FindAsync(p =>
                    p.OwnerPeerId == query.OwnerPeerId && p.ToPeerId == query.ToPeerId && p.MessageId < maxId,
                findOptions,
                cancellationToken);
        return await cursor.ToListAsync(cancellationToken);
    }
}
