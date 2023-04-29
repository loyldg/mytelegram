namespace MyTelegram.QueryHandlers.MongoDB.User;

// ReSharper disable once UnusedMember.Global
public class SearchUserByKeywordQueryHandler : IQueryHandler<SearchUserByKeywordQuery, IReadOnlyCollection<IUserReadModel>>
{
    private readonly IMongoDbReadModelStore<UserReadModel> _store;

    public SearchUserByKeywordQueryHandler(IMongoDbReadModelStore<UserReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IUserReadModel>> ExecuteQueryAsync(SearchUserByKeywordQuery query,
        CancellationToken cancellationToken)
    {
        var q = query.Keyword;
        if (!string.IsNullOrEmpty(q) && q.StartsWith("@"))
        {
            q = query.Keyword[1..];
        }

        Expression<Func<UserReadModel, bool>> predicate = x => true;
        predicate = predicate.WhereIf(!string.IsNullOrEmpty(q),
            p => (p.UserName != null && p.UserName.StartsWith(q)) || p.FirstName.Contains(q));
        var options = new FindOptions<UserReadModel, UserReadModel>
        {
            Limit = 50,
            Skip = 0,
            Sort = Builders<UserReadModel>.Sort.Ascending(p => p.UserName)
        };

        var cursor = await _store.FindAsync(predicate, options, cancellationToken);
        return await cursor.ToListAsync(cancellationToken);
    }
}
