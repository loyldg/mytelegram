namespace MyTelegram.QueryHandlers.MongoDB.ChatInviteImporter;

public class GetChatInviteImporterQueryHandler : IQueryHandler<GetChatInviteImporterQuery,
    IChatInviteImporterReadModel?>
{
    private readonly IMyMongoDbReadModelStore<ChatInviteImporterReadModel> _store;

    public GetChatInviteImporterQueryHandler(IMyMongoDbReadModelStore<ChatInviteImporterReadModel> store)
    {
        _store = store;
    }

    public async Task<IChatInviteImporterReadModel?> ExecuteQueryAsync(GetChatInviteImporterQuery query, CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.PeerId == query.PeerId && p.UserId == query.UserId, cancellationToken: cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}