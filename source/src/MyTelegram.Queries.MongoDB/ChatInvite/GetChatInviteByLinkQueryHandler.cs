namespace MyTelegram.QueryHandlers.MongoDB.ChatInvite;

public class GetChatInviteByLinkQueryHandler : IQueryHandler<GetChatInviteByLinkQuery, IChatInviteReadModel?>
{
    private readonly IMyMongoDbReadModelStore<ChatInviteReadModel> _store;

    public GetChatInviteByLinkQueryHandler(IMyMongoDbReadModelStore<ChatInviteReadModel> store)
    {
        _store = store;
    }

    public async Task<IChatInviteReadModel?> ExecuteQueryAsync(GetChatInviteByLinkQuery query, CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.Link == query.Link, cancellationToken: cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }
}