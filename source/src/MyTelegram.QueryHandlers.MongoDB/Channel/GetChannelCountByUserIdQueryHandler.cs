namespace MyTelegram.QueryHandlers.MongoDB.Channel;

public class GetChannelCountByUserIdQueryHandler(IQueryOnlyReadModelStore<ChannelReadModel> store) : IQueryHandler<GetChannelCountByUserIdQuery, int>
{
    public async Task<int> ExecuteQueryAsync(GetChannelCountByUserIdQuery query, CancellationToken cancellationToken)
    {
        return (int)await store.CountAsync(p => p.CreatorId == query.UserId, cancellationToken);
    }
}