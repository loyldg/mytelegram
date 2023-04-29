namespace MyTelegram.QueryHandlers.MongoDB.Poll;

public class GetPollsQueryHandler : IQueryHandler<GetPollsQuery, IReadOnlyCollection<IPollReadModel>>
{
    private readonly IMongoDbReadModelStore<PollReadModel> _store;

    public GetPollsQueryHandler(IMongoDbReadModelStore<PollReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IPollReadModel>> ExecuteQueryAsync(GetPollsQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => query.PollIds.Contains(p.PollId), cancellationToken: cancellationToken)
            ;

        return await cursor.ToListAsync(cancellationToken);
    }
}