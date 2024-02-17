namespace MyTelegram.QueryHandlers.MongoDB.ChatInvite;

public class GetChatInviteQueryHandler : IQueryHandler<GetChatInviteQuery, IChatInviteReadModel?>
{
    private readonly IMyMongoDbReadModelStore<ChatInviteReadModel> _store;

    public GetChatInviteQueryHandler(IMyMongoDbReadModelStore<ChatInviteReadModel> store)
    {
        _store = store;
    }

    public async Task<IChatInviteReadModel?> ExecuteQueryAsync(GetChatInviteQuery query, CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.PeerId == query.PeerId && p.Link == query.Link, cancellationToken: cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }
}