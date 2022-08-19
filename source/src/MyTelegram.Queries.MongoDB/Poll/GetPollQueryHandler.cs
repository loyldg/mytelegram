namespace MyTelegram.QueryHandlers.MongoDB.Poll;

public class GetPollQueryHandler : IQueryHandler<GetPollQuery, IPollReadModel?>
{
    private readonly IMongoDbReadModelStore<PollReadModel> _store;

    public GetPollQueryHandler(IMongoDbReadModelStore<PollReadModel> store)
    {
        _store = store;
    }

    public async Task<IPollReadModel?> ExecuteQueryAsync(GetPollQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.ToPeerId == query.ToPeerId && p.PollId == query.PollId,
            cancellationToken: cancellationToken).ConfigureAwait(false);
        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }
}