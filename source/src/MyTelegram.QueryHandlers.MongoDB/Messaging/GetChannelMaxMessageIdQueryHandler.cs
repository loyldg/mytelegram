namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

public class GetChannelMaxMessageIdQueryHandler(IQueryOnlyReadModelStore<MessageReadModel> store) : IQueryHandler<GetChannelMaxMessageIdQuery, int>
{
    public async Task<int> ExecuteQueryAsync(GetChannelMaxMessageIdQuery query, CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p => p.OwnerPeerId == query.ChannelId,
            p => p.MessageId,
            sort: new SortOptions<MessageReadModel>(p => p.MessageId, SortType.Descending)
            , cancellationToken);
    }
}