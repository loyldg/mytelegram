namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

// ReSharper disable once UnusedMember.Global
public class
    GetReadHistoryMessageQueryHandler : IQueryHandler<GetReadHistoryMessageQuery, IMessageReadModel?>
{
    private readonly IMongoDbReadModelStore<MessageReadModel> _store;

    public GetReadHistoryMessageQueryHandler(IMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<IMessageReadModel?> ExecuteQueryAsync(GetReadHistoryMessageQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(
            p => p.OwnerPeerId == query.OwnerPeerId && p.MessageId == query.MessageId && p.ToPeerId == query.ToPeerId,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return await cursor.SingleOrDefaultAsync(cancellationToken).ConfigureAwait(false);
    }
}
