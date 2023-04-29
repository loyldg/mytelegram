namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

// ReSharper disable once UnusedMember.Global
public class
    GetReadingHistoryQueryHandler : IQueryHandler<GetReadingHistoryQuery, IReadOnlyCollection<long>>
{
    private readonly IMyMongoDbReadModelStore<ReadingHistoryReadModel> _store;

    public GetReadingHistoryQueryHandler(IMyMongoDbReadModelStore<ReadingHistoryReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetReadingHistoryQuery query,
        CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<ReadingHistoryReadModel, long>
        {
            Projection = new ProjectionDefinitionBuilder<ReadingHistoryReadModel>().Expression(p => p.TargetPeerId),
            Limit = 200 //query.Limit
        };
        var cursor = await _store
                .FindAsync(p => p.TargetPeerId == query.TargetPeerId && p.MessageId == query.MessageId,
                    findOptions,
                    cancellationToken)
            ;

        return await cursor.ToListAsync(cancellationToken);
    }
}
