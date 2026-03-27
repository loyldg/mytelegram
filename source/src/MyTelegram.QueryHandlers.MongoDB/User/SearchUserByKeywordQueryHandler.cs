using System.Text.RegularExpressions;

namespace MyTelegram.QueryHandlers.MongoDB.User;

public class SearchUserByKeywordQueryHandler(IQueryOnlyReadModelStore<UserReadModel> store) :
    IQueryHandler<SearchUserByKeywordQuery, IReadOnlyCollection<IUserReadModel>>
{
    public async Task<IReadOnlyCollection<IUserReadModel>> ExecuteQueryAsync(SearchUserByKeywordQuery query,
        CancellationToken cancellationToken)
    {
        var q = query.Keyword;
        if (!string.IsNullOrEmpty(q) && q.StartsWith('@'))
        {
            q = query.Keyword[1..];
        }

        Expression<Func<UserReadModel, bool>> predicate = x => true;
        predicate = predicate.WhereIf(!string.IsNullOrEmpty(q),
            p => (p.UserName != null && Regex.IsMatch(p.UserName, $"^{q}", RegexOptions.IgnoreCase)) ||
                 Regex.IsMatch(p.FirstName, $"{q}", RegexOptions.IgnoreCase) ||
                 (p.LastName != null && Regex.IsMatch(p.LastName, $"{q}", RegexOptions.IgnoreCase))
                 );

        return await store.FindAsync(predicate, 0, 50, new SortOptions<UserReadModel>(p => p.FirstName, SortType.Ascending), cancellationToken);
    }
}