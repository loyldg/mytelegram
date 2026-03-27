using System.Text.RegularExpressions;

namespace MyTelegram.QueryHandlers.MongoDB.UserName;

public class SearchUserNameQueryHandler(IQueryOnlyReadModelStore<UserNameReadModel> store) : IQueryHandler<SearchUserNameQuery, IReadOnlyCollection<IUserNameReadModel>>
{
    public async Task<IReadOnlyCollection<IUserNameReadModel>> ExecuteQueryAsync(SearchUserNameQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FindAsync(p => Regex.IsMatch(p.UserName, $"^{query.Keyword}", RegexOptions.IgnoreCase),
            limit: 50, cancellationToken: cancellationToken);
    }
}