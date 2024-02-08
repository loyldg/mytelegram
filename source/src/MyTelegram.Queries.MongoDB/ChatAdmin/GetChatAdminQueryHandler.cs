namespace MyTelegram.QueryHandlers.MongoDB.ChatAdmin;

public class GetChatAdminQueryHandler : IQueryHandler<GetChatAdminQuery, IChatAdminReadModel?>
{
    private readonly IMyMongoDbReadModelStore<ChatAdminReadModel> _store;

    public GetChatAdminQueryHandler(IMyMongoDbReadModelStore<ChatAdminReadModel> store)
    {
        _store = store;
    }

    public async Task<IChatAdminReadModel?> ExecuteQueryAsync(GetChatAdminQuery query, CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.PeerId == query.PeerId && p.UserId == query.AdminId, cancellationToken: cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}