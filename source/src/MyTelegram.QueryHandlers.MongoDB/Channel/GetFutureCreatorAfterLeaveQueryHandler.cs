namespace MyTelegram.QueryHandlers.MongoDB.Channel;

public class GetFutureCreatorAfterLeaveQueryHandler
    (IQueryOnlyReadModelStore<ChannelMemberReadModel> store) : IQueryHandler<GetFutureCreatorAfterLeaveQuery, long?>
{
    public async Task<long?> ExecuteQueryAsync(GetFutureCreatorAfterLeaveQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p => p.ChannelId == query.ChannelId && p.UserId != query.CurrentCreatorUserId, p => p.UserId, new SortOptions<ChannelMemberReadModel>(p => p.Date, SortType.Ascending), cancellationToken);
    }
}