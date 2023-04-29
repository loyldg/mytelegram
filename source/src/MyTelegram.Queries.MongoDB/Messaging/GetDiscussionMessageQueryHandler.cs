namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

public class GetDiscussionMessageQueryHandler : IQueryHandler<GetDiscussionMessageQuery, IMessageReadModel?>
{
    private readonly IMongoDbReadModelStore<MessageReadModel> _store;

    public GetDiscussionMessageQueryHandler(IMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<IMessageReadModel?> ExecuteQueryAsync(GetDiscussionMessageQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p =>
                p.FwdHeader!.SavedFromPeer!.PeerId == query.SavedFromPeerId &&
                p.FwdHeader.SavedFromMsgId == query.SavedFromMessageId,
            cancellationToken: cancellationToken);
        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }
}
