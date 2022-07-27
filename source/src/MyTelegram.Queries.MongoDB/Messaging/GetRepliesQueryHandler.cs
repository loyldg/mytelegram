namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

public class GetRepliesQueryHandler : IQueryHandler<GetRepliesQuery, IReadOnlyCollection<IReplyReadModel>>
{
    private readonly IMongoDbReadModelStore<ReplyReadModel> _store;

    public GetRepliesQueryHandler(IMongoDbReadModelStore<ReplyReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IReplyReadModel>> ExecuteQueryAsync(GetRepliesQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store
            .FindAsync(p => p.SavedFromPeerId == query.ChannelId && query.MessageIds.Contains(p.SavedFromMsgId), cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return await cursor.ToListAsync(cancellationToken);
    }
}