namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

public class GetReplyQueryHandler : IQueryHandler<GetReplyQuery, IReplyReadModel?>
{
    private readonly IMongoDbReadModelStore<ReplyReadModel> _store;

    public GetReplyQueryHandler(IMongoDbReadModelStore<ReplyReadModel> store)
    {
        _store = store;
    }

    public async Task<IReplyReadModel?> ExecuteQueryAsync(GetReplyQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p =>
                p.SavedFromPeerId == query.ChannelId && p.SavedFromMsgId == query.SavedFromMsgId,
            cancellationToken: cancellationToken);
        return await cursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}