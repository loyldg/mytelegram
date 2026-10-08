namespace MyTelegram.QueryHandlers.MongoDB.User;

public class GetMaxUserIdQueryHandler(IQueryOnlyReadModelStore<UserReadModel> store) : IQueryHandler<GetMaxUserIdQuery, long>
{
    public async Task<long> ExecuteQueryAsync(GetMaxUserIdQuery query, CancellationToken cancellationToken)
    {
        var maxId = await store.FirstOrDefaultAsync(p => p.UserId > 0 && !p.Bot && p.UserId < 600000000000, createResult: p => p.UserId, sort: new SortOptions<UserReadModel>(p => p.UserId, SortType.Descending), cancellationToken: cancellationToken);
        if (maxId > 0)
        {
            maxId -= MyTelegramConsts.UserIdBase;
        }
        else
        {
            maxId = 0;
        }

        return maxId;
    }
}