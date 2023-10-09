namespace MyTelegram.QueryHandlers.MongoDB.UserName;

public class
    GetUserNameListByNamesQueryHandler : IQueryHandler<GetUserNameListByNamesQuery,
        IReadOnlyCollection<IUserNameReadModel>>
{
    private readonly IMongoDbReadModelStore<UserNameReadModel> _store;

    public GetUserNameListByNamesQueryHandler(IMongoDbReadModelStore<UserNameReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IUserNameReadModel>> ExecuteQueryAsync(GetUserNameListByNamesQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<UserNameReadModel, bool>> predicate = p => query.UserNames.Contains(p.UserName);
        if (query.PeerType.HasValue)
        {
            predicate = predicate.And(p => p.PeerType == query.PeerType.Value);
        }
        var cursor = await _store.FindAsync(predicate, cancellationToken: cancellationToken);

        return await cursor.ToListAsync(cancellationToken);
    }
}