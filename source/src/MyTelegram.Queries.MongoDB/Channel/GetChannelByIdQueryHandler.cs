namespace MyTelegram.QueryHandlers.MongoDB.Channel;

public class GetChannelByIdQueryHandler : IQueryHandler<GetChannelByIdQuery, IChannelReadModel>
{
    private readonly IMongoDbReadModelStore<ChannelReadModel> _store;

    public GetChannelByIdQueryHandler(IMongoDbReadModelStore<ChannelReadModel> store)
    {
        _store = store;
    }

    public async Task<IChannelReadModel> ExecuteQueryAsync(GetChannelByIdQuery query,
        CancellationToken cancellationToken)
    {
        var readMode = await _store.GetAsync(ChannelId.Create(query.ChannelId).Value, cancellationToken)
            ;
        return readMode.ReadModel;
    }
}
