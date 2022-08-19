namespace MyTelegram.QueryHandlers.MongoDB.Poll;

public class
    GetChosenVoteAnswersQueryHandler : IQueryHandler<GetChosenVoteAnswersQuery,
        IReadOnlyCollection<IPollAnswerVoterReadModel>>
{
    private readonly IMyMongoDbReadModelStore<PollAnswerVoterReadModel> _store;

    public GetChosenVoteAnswersQueryHandler(IMyMongoDbReadModelStore<PollAnswerVoterReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IPollAnswerVoterReadModel>> ExecuteQueryAsync(GetChosenVoteAnswersQuery query,
        CancellationToken cancellationToken)
    {
        //var findOptions = new FindOptions<PollAnswerVoterReadModel, string>
        //{
        //    Projection = new ProjectionDefinitionBuilder<PollAnswerVoterReadModel>().Expression(p => p.Option),
        //    Limit = MyTelegramServerDomainConsts.MaxVoteOptions //query.Limit
        //};

        var cursor = await _store.FindAsync(p => query.PollIds.Contains(p.PollId) && p.VoterPeerId == query.VoterPeerId, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await cursor.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}