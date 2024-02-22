namespace MyTelegram.QueryHandlers.MongoDB.Contact;

public class
    GetContactsByUidQueryHandler : IQueryHandler<GetContactsByUserIdQuery, IReadOnlyCollection<IContactReadModel>>
{
    private readonly IMongoDbReadModelStore<ContactReadModel> _store;

    public GetContactsByUidQueryHandler(IMongoDbReadModelStore<ContactReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IContactReadModel>> ExecuteQueryAsync(GetContactsByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.SelfUserId == query.UserId, cancellationToken: cancellationToken)
     ;

        return await cursor.ToListAsync(cancellationToken);
    }
}
