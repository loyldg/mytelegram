namespace MyTelegram.QueryHandlers.MongoDB.Poll;

public class
    GetPollVotesQueryHandler(IQueryOnlyReadModelStore<PollAnswerVoterReadModel> store) : IQueryHandler<GetPollVotesQuery,
    IReadOnlyCollection<IPollAnswerVoterReadModel>>
{
    public async Task<IReadOnlyCollection<IPollAnswerVoterReadModel>> ExecuteQueryAsync(GetPollVotesQuery query,
        CancellationToken cancellationToken)
    {
        Expression<Func<PollAnswerVoterReadModel, bool>> predicate = p => p.PollId == query.PollId;
        predicate = predicate.WhereIf(!string.IsNullOrEmpty(query.Option), p => p.Option == query.Option);


        return await store.FindAsync(predicate, skip: query.Skip, limit: query.Limit, cancellationToken: cancellationToken);
    }
}