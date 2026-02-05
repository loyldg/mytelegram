namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

public class GetMessageByBatchIdQueryHandler(IQueryOnlyReadModelStore<MessageReadModel> store) : IQueryHandler<GetMessageByBatchIdQuery, IMessageReadModel?>
{
    public async Task<IMessageReadModel?> ExecuteQueryAsync(GetMessageByBatchIdQuery query, CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p => p.BatchId == query.BatchId && p.OwnerPeerId != query.ExcludePeerId, cancellationToken);
    }
}