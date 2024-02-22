namespace MyTelegram.QueryHandlers.MongoDB.Contact;

public class
    GetContactsByPhonesQueryHandler : IQueryHandler<GetContactsByPhonesQuery, IReadOnlyCollection<IContactReadModel>>
{
    private readonly IMyMongoDbReadModelStore<ContactReadModel> _store;

    public GetContactsByPhonesQueryHandler(IMyMongoDbReadModelStore<ContactReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IContactReadModel>> ExecuteQueryAsync(GetContactsByPhonesQuery query, CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.SelfUserId == query.SelfUserId && query.Phones.Contains(p.Phone), cancellationToken: cancellationToken);

        return await cursor.ToListAsync(cancellationToken);
    }
}