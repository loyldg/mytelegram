namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

public class
    GetMessagesByUserQueryHandler : IQueryHandler<GetMessagesByUserIdQuery,
        IReadOnlyCollection<IMessageReadModel>>
{
    private readonly IMongoDbReadModelStore<MessageReadModel> _store;

    public GetMessagesByUserQueryHandler(IMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IMessageReadModel>> ExecuteQueryAsync(GetMessagesByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.OwnerPeerId == query.OwnerPeerId && p.ToPeerId == query.ToPeerId,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        return await cursor.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}