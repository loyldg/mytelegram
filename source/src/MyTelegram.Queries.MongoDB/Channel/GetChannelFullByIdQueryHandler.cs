namespace MyTelegram.QueryHandlers.MongoDB.Channel;

public class GetChannelFullByIdQueryHandler : IQueryHandler<GetChannelFullByIdQuery, IChannelFullReadModel?>
{
    private readonly IMongoDbReadModelStore<ChannelFullReadModel> _store;

    public GetChannelFullByIdQueryHandler(IMongoDbReadModelStore<ChannelFullReadModel> store)
    {
        _store = store;
    }

    public async Task<IChannelFullReadModel?> ExecuteQueryAsync(GetChannelFullByIdQuery query,
        CancellationToken cancellationToken)
    {
        var channelFull = await _store.GetAsync(ChannelId.Create(query.ChannelId).Value, cancellationToken)
            .ConfigureAwait(false);
        return channelFull.ReadModel;
    }
}
