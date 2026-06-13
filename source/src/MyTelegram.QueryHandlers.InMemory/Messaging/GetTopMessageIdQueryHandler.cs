namespace MyTelegram.QueryHandlers.InMemory.Messaging;

public class GetTopMessageIdQueryHandler(IQueryOnlyReadModelStore<MessageReadModel> store) : IQueryHandler<GetTopMessageIdQuery, int>
{
    public Task<int> ExecuteQueryAsync(GetTopMessageIdQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<MessageReadModel, bool>> filter = p => p.OwnerPeerId == query.OwnerPeerId && !query.MessageIds.Contains(p.MessageId);
        if (!query.OwnerPeerId.IsChannelPeer())
        {
            filter = filter.And(p => p.ToPeerId == query.ToPeerId);
        }

        return store.FirstOrDefaultAsync(
            filter, p => p.MessageId,
            new SortOptions<MessageReadModel>(p => p.MessageId, SortType.Descending), cancellationToken);
    }
}