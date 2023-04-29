namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

// ReSharper disable once UnusedMember.Global
public class GetMessageByIdQueryHandler : IQueryHandler<GetMessageByIdQuery, IMessageReadModel?>
{
    private readonly IMongoDbReadModelStore<MessageReadModel> _store;

    public GetMessageByIdQueryHandler(IMongoDbReadModelStore<MessageReadModel> store)
    {
        _store = store;
    }

    public async Task<IMessageReadModel?> ExecuteQueryAsync(GetMessageByIdQuery query,
        CancellationToken cancellationToken)
    {
        var item = await _store.GetAsync(query.Id, cancellationToken);
        return item.ReadModel;
    }
}