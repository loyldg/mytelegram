namespace MyTelegram.QueryHandlers.MongoDB.ChatInvite;

public class
    GetRevokedChatInvitesQueryHandler : IQueryHandler<GetRevokedChatInvitesQuery,
        IReadOnlyCollection<IChatInviteReadModel>>
{
    private readonly IMyMongoDbReadModelStore<ChatInviteReadModel> _store;

    public GetRevokedChatInvitesQueryHandler(IMyMongoDbReadModelStore<ChatInviteReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IChatInviteReadModel>> ExecuteQueryAsync(GetRevokedChatInvitesQuery query, CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.PeerId == query.PeerId && p.Revoked && p.AdminId == query.AdminId, cancellationToken: cancellationToken);

        return await cursor.ToListAsync(cancellationToken: cancellationToken);
    }
}