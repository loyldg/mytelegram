namespace MyTelegram.QueryHandlers.MongoDB.Channel;

public class GetPermanentChatInviteQueryHandler : IQueryHandler<GetPermanentChatInviteQuery, IChatInviteReadModel?>
{
    private readonly IMyMongoDbReadModelStore<ChatInviteReadModel> _store;

    public GetPermanentChatInviteQueryHandler(IMyMongoDbReadModelStore<ChatInviteReadModel> store)
    {
        _store = store;
    }

    public async Task<IChatInviteReadModel?> ExecuteQueryAsync(GetPermanentChatInviteQuery query, CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.PeerId == query.PeerId && p.Permanent, cancellationToken: cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }
}