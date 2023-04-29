namespace MyTelegram.QueryHandlers.MongoDB.Chat;

// ReSharper disable once UnusedMember.Global
public class GetChatByChatIdListQueryHandler : IQueryHandler<GetChatByChatIdListQuery, IReadOnlyList<IChatReadModel>>
{
    private readonly IMongoDbReadModelStore<ChatReadModel> _store;

    public GetChatByChatIdListQueryHandler(IMongoDbReadModelStore<ChatReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyList<IChatReadModel>> ExecuteQueryAsync(GetChatByChatIdListQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store
                .FindAsync(p => query.ChatIdList.Contains(p.ChatId), cancellationToken: cancellationToken)
            ;
        return await cursor.ToListAsync(cancellationToken);
    }
}
