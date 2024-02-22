namespace MyTelegram.QueryHandlers.MongoDB.Contact;

public class SearchContactQueryHandler : IQueryHandler<SearchContactQuery, IReadOnlyCollection<IContactReadModel>>
{
    private readonly IMongoDbReadModelStore<ContactReadModel> _store;

    public SearchContactQueryHandler(IMongoDbReadModelStore<ContactReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IContactReadModel>> ExecuteQueryAsync(SearchContactQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p =>
                    (p.SelfUserId == query.SelfUserId && (p.FirstName.Contains(query.Keyword) || p.Phone.Contains(query.Keyword) || (p.LastName != null && p.LastName.Contains(query.Keyword)))),
                cancellationToken: cancellationToken)
     ;
        return await cursor.ToListAsync(cancellationToken);
    }
}
