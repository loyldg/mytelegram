namespace MyTelegram.QueryHandlers.MongoDB.Poll;

public class
    GetPollAnswerVotersQueryHandler : IQueryHandler<GetPollAnswerVotersQuery,
        IReadOnlyCollection<IPollAnswerVoterReadModel>>
{
    private readonly IMongoDbReadModelStore<PollAnswerVoterReadModel> _store;

    public GetPollAnswerVotersQueryHandler(IMongoDbReadModelStore<PollAnswerVoterReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IPollAnswerVoterReadModel>> ExecuteQueryAsync(GetPollAnswerVotersQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.PollId == query.PollId && p.VoterPeerId == query.VoterPeerId, cancellationToken: cancellationToken);
        return await cursor.ToListAsync(cancellationToken);
    }
}