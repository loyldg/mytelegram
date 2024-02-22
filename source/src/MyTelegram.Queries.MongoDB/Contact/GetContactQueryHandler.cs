namespace MyTelegram.QueryHandlers.MongoDB.Contact;

public class GetContactQueryHandler : IQueryHandler<GetContactQuery, IContactReadModel?>
{
    private readonly IMongoDbReadModelStore<ContactReadModel> _store;

    public GetContactQueryHandler(IMongoDbReadModelStore<ContactReadModel> store)
    {
        _store = store;
    }

    public async Task<IContactReadModel?> ExecuteQueryAsync(GetContactQuery query,
        CancellationToken cancellationToken)
    {
        var item = await _store
            .GetAsync(ContactId.Create(query.SelfUserId, query.TargetUid).Value, CancellationToken.None)
     ;

        return item.ReadModel;
    }
}
