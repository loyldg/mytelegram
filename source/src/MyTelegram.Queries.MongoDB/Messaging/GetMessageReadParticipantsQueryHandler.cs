namespace MyTelegram.QueryHandlers.MongoDB.Messaging;

public class GetMessageReadParticipantsQueryHandler : IQueryHandler<GetMessageReadParticipantsQuery,
    IReadOnlyCollection<IReadingHistoryReadModel>>
{
    private readonly IMyMongoDbReadModelStore<ReadingHistoryReadModel> _store;

    public GetMessageReadParticipantsQueryHandler(IMyMongoDbReadModelStore<ReadingHistoryReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IReadingHistoryReadModel>> ExecuteQueryAsync(GetMessageReadParticipantsQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.TargetPeerId == query.TargetPeerId && p.MessageId == query.MessageId, cancellationToken: cancellationToken)
            ;

        return await cursor.ToListAsync(cancellationToken: cancellationToken);
    }
}
