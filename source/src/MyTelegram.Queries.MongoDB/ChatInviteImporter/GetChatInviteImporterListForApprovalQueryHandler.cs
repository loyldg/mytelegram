namespace MyTelegram.QueryHandlers.MongoDB.ChatInviteImporter;

public class GetChatInviteImporterListForApprovalQueryHandler : IQueryHandler<GetChatInviteImporterListForApprovalQuery,
    IReadOnlyCollection<IChatInviteImporterReadModel>>
{
    private readonly IMyMongoDbReadModelStore<ChatInviteImporterReadModel> _store;

    public GetChatInviteImporterListForApprovalQueryHandler(IMyMongoDbReadModelStore<ChatInviteImporterReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IChatInviteImporterReadModel>> ExecuteQueryAsync(GetChatInviteImporterListForApprovalQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<ChatInviteImporterReadModel, bool>> predicate = x => x.PeerId == query.PeerId && x.ChatInviteRequestState == ChatInviteRequestState.NeedApprove;


        var cursor = await _store.FindAsync(p => p.PeerId == query.PeerId && p.InviteId == query.InviteId && p.ChatInviteRequestState == ChatInviteRequestState.NeedApprove, cancellationToken: cancellationToken);

        return await cursor.ToListAsync(cancellationToken: cancellationToken);
    }
}