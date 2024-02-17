namespace MyTelegram.QueryHandlers.MongoDB.ChatInvite;

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
        var date = query.OffsetDate ?? 0;
        var findOptions = new FindOptions<ChatInviteReadModel, ChatInviteReadModel>()
        {
            Limit = query.Limit,
        };

        var cursor = await _store.FindAsync(p =>
                    p.Revoked == query.Revoked &&
                    p.PeerId == query.PeerId &&
                    p.AdminId == query.AdminId &&
                    p.Date > date
                ,
                findOptions,
                cancellationToken: cancellationToken)
     ;

        return await cursor.ToListAsync(cancellationToken);
    }
}