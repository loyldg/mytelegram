namespace MyTelegram.QueryHandlers.MongoDB.Chat;

public class GetChatByChatIdQueryHandler : IQueryHandler<GetChatByChatIdQuery, IChatReadModel?>
{
    private readonly IMongoDbReadModelStore<ChatReadModel> _store;

    public GetChatByChatIdQueryHandler(IMongoDbReadModelStore<ChatReadModel> store)
    {
        _store = store;
    }

    public async Task<IChatReadModel?> ExecuteQueryAsync(GetChatByChatIdQuery query,
        CancellationToken cancellationToken)
    {
        var item = await _store.GetAsync(ChatId.Create(query.ChatId).Value, cancellationToken);
        return item.ReadModel;
    }
}
