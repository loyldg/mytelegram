namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

public class GetOutboxReadDateQueryHandler : IQueryHandler<GetOutboxReadDateQuery, int>
{
    private readonly IMyMongoDbReadModelStore<ReadingHistoryReadModel> _store;

    public GetOutboxReadDateQueryHandler(IMyMongoDbReadModelStore<ReadingHistoryReadModel> store)
    {
        _store = store;
    }

    public async Task<int> ExecuteQueryAsync(GetOutboxReadDateQuery query, CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<ReadingHistoryReadModel, int>
        {
            Projection = Builders<ReadingHistoryReadModel>.Projection.Expression(p => p.Date)
        };

        var cursor = await _store.FindAsync(p =>
            p.ReaderPeerId == query.ToPeer.PeerId&&
            p.TargetPeerId == query.UserId && 
            p.MessageId == query.MessageId, findOptions, cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}