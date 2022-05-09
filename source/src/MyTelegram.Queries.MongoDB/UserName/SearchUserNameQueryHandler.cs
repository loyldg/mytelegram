namespace MyTelegram.QueryHandlers.MongoDB.UserName;

// ReSharper disable once UnusedMember.Global
public class SearchUserNameQueryHandler : IQueryHandler<SearchUserNameQuery, IReadOnlyCollection<IUserNameReadModel>>
{
    private readonly IMongoDbReadModelStore<UserNameReadModel> _store;

    public SearchUserNameQueryHandler(IMongoDbReadModelStore<UserNameReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IUserNameReadModel>> ExecuteQueryAsync(SearchUserNameQuery query,
        CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<UserNameReadModel, UserNameReadModel>
        {
            Limit = 50
        };

        var cursor = await _store.FindAsync(p => p.UserName.StartsWith(query.Keyword),
            findOptions,
            cancellationToken).ConfigureAwait(false);

        return await cursor.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}
