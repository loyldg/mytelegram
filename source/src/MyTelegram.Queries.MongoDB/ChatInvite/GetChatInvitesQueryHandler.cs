namespace MyTelegram.QueryHandlers.MongoDB.ChatInvite;

// ReSharper disable once UnusedMember.Global
public class
    GetChatInvitesQueryHandler : IQueryHandler<GetChatInvitesQuery, IReadOnlyCollection<IChatInviteReadModel>>
{
    private readonly IMongoDbReadModelStore<ChatInviteReadModel> _store;

    public GetChatInvitesQueryHandler(IMongoDbReadModelStore<ChatInviteReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IChatInviteReadModel>> ExecuteQueryAsync(GetChatInvitesQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p =>
                    p.Revoked == query.Revoked && p.PeerId == query.PeerId && p.AdminId == query.AdminId,
                cancellationToken: cancellationToken)
            ;

        return await cursor.ToListAsync(cancellationToken);
    }
}

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