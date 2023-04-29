namespace MyTelegram.QueryHandlers.MongoDB.Channel;

public class GetLinkedChannelIdQueryHandler : IQueryHandler<GetLinkedChannelIdQuery, long?>
{
    private readonly IMyMongoDbReadModelStore<ChannelFullReadModel> _store;

    public GetLinkedChannelIdQueryHandler(IMyMongoDbReadModelStore<ChannelFullReadModel> store)
    {
        _store = store;
    }

    public async Task<long?> ExecuteQueryAsync(GetLinkedChannelIdQuery query,
        CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<ChannelFullReadModel, long?>
        {
            Projection = new ProjectionDefinitionBuilder<ChannelFullReadModel>().Expression(p => p.LinkedChatId),
            Limit = 100
        };
        var cursor = await _store.FindAsync(p => p.ChannelId == query.ChannelId, findOptions, cancellationToken: cancellationToken);
        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }
}
