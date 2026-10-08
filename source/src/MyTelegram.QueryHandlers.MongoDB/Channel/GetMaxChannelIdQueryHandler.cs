namespace MyTelegram.QueryHandlers.MongoDB.Channel;

public class GetMaxChannelIdQueryHandler(IQueryOnlyReadModelStore<ChannelReadModel> store) : IQueryHandler<GetMaxChannelIdQuery, long>
{
    public async Task<long> ExecuteQueryAsync(GetMaxChannelIdQuery query, CancellationToken cancellationToken)
    {
        var id = (await store.FirstOrDefaultAsync(p => p.ChannelId < 900000000000, p => p.ChannelId,
            sort: new SortOptions<ChannelReadModel>(p => p.ChannelId, SortType.Descending), cancellationToken: cancellationToken));

        if (id > 0)
        {
            id -= MyTelegramConsts.ChannelIdBase;
        }
        else
        {
            id = 0;
        }

        return id;
    }
}