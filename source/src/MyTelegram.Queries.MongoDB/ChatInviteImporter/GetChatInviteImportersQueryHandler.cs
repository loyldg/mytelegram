namespace MyTelegram.QueryHandlers.MongoDB.ChatInviteImporter;
public class GetChatInviteImportersQueryHandler : IQueryHandler<GetChatInviteImportersQuery, IReadOnlyCollection<IChatInviteImporterReadModel>>
{
    private readonly IMyMongoDbReadModelStore<ChatInviteImporterReadModel> _store;

    public GetChatInviteImportersQueryHandler(IMyMongoDbReadModelStore<ChatInviteImporterReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IChatInviteImporterReadModel>> ExecuteQueryAsync(GetChatInviteImportersQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<ChatInviteImporterReadModel, bool>> predicate = x => x.PeerId == query.PeerId &&
                                                                             //x.ChatInviteRequestState == query.ChatInviteRequestState &&
                                                                             x.Date > query.OffsetDate;
        predicate = predicate.WhereIf(query.ChatInviteRequestState.HasValue,
            x => x.ChatInviteRequestState == query.ChatInviteRequestState);
        var findOptions = new FindOptions<ChatInviteImporterReadModel, ChatInviteImporterReadModel>
        {
            Limit = query.Limit
        };

        var cursor = await _store.FindAsync(predicate, findOptions, cancellationToken);

        return await cursor.ToListAsync(cancellationToken: cancellationToken);
    }
}