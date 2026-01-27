namespace MyTelegram.QueryHandlers.MongoDB.Poll;

public class GetPollQueryHandler(IQueryOnlyReadModelStore<PollReadModel> store) : IQueryHandler<GetPollQuery, IPollReadModel?>
{
    public async Task<IPollReadModel?> ExecuteQueryAsync(GetPollQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p => p.PollId == query.PollId,
            cancellationToken: cancellationToken);
    }
}