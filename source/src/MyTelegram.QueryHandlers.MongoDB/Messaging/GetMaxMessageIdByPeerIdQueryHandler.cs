namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

public class GetMaxMessageIdByPeerIdQueryHandler(IQueryOnlyReadModelStore<MessageReadModel> store) : IQueryHandler<GetMaxMessageIdByPeerIdQuery, int>
{
    public async Task<int> ExecuteQueryAsync(GetMaxMessageIdByPeerIdQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p => p.OwnerPeerId == query.PeerId, p => p.MessageId,
            sort: new SortOptions<MessageReadModel>(m => m.MessageId, SortType.Descending),
            cancellationToken: cancellationToken);
    }
}