namespace MyTelegram.QueryHandlers.MongoDB.User;

// ReSharper disable once UnusedMember.Global
public class GetUsersByUidListQueryHandler : IQueryHandler<GetUsersByUidListQuery, IReadOnlyList<IUserReadModel>>
{
    private readonly IMongoDbReadModelStore<UserReadModel> _store;

    public GetUsersByUidListQueryHandler(IMongoDbReadModelStore<UserReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyList<IUserReadModel>> ExecuteQueryAsync(GetUsersByUidListQuery query,
        CancellationToken cancellationToken)
    {
        if (query.UidList.Count == 0)
        {
            return new List<UserReadModel>();
        }

        var cursor =
                await _store.FindAsync(p => query.UidList.Contains(p.UserId), cancellationToken: cancellationToken)
                    .ConfigureAwait(false)
            ;
        return await cursor.ToListAsync(cancellationToken);
    }
}
