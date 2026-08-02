namespace MyTelegram.QueryHandlers.MongoDB.Channel;

public class GetMaxChannelIdQueryHandler(IQueryOnlyReadModelStore<ChannelReadModel> store) : IQueryHandler<GetMaxChannelIdQuery, long>
{
    public async Task<long> ExecuteQueryAsync(GetMaxChannelIdQuery query, CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p => p.Broadcast || p.MegaGroup, p => p.ChannelId,
            sort: new SortOptions<ChannelReadModel>(p => p.ChannelId, SortType.Descending), cancellationToken: cancellationToken);
    }
}