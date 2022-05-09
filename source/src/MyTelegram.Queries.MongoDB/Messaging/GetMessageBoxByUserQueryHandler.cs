namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

// ReSharper disable once UnusedMember.Global
public class GetMessageByIdQueryHandler : IQueryHandler<GetMessageByIdQuery, IMessageReadModel?>
{
    private readonly IMongoDbReadModelStore<MessageReadModel> _store;

    public GetMessageByIdQueryHandler(IMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<IMessageReadModel?> ExecuteQueryAsync(GetMessageByIdQuery query,
        CancellationToken cancellationToken)
    {
        var item = await _store.GetAsync(query.Id, cancellationToken).ConfigureAwait(false);
        return item.ReadModel;
    }
}

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
