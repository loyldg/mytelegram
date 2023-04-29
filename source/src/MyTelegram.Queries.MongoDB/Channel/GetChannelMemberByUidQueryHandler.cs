namespace MyTelegram.QueryHandlers.MongoDB.Channel;

public class GetChannelMemberByUidQueryHandler : IQueryHandler<GetChannelMemberByUidQuery, IChannelMemberReadModel?>
{
    private readonly IMongoDbReadModelStore<ChannelMemberReadModel> _store;

    public GetChannelMemberByUidQueryHandler(IMongoDbReadModelStore<ChannelMemberReadModel> store)
    {
        _store = store;
    }

    public async Task<IChannelMemberReadModel?> ExecuteQueryAsync(GetChannelMemberByUidQuery query,
        CancellationToken cancellationToken)
    {
        var id = ChannelMemberId.Create(query.ChannelId, query.UserId);
        var item = await _store.GetAsync(id.Value, cancellationToken);
        return item.ReadModel;
    }
}
