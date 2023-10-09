namespace MyTelegram.QueryHandlers.MongoDB.UserName;

public class
    GetUserNameByNameQueryHandler : IQueryHandler<GetUserNameByNameQuery, IUserNameReadModel?>
{
    private readonly IMongoDbReadModelStore<UserNameReadModel> _store;

    public GetUserNameByNameQueryHandler(IMongoDbReadModelStore<UserNameReadModel> store)
    {
        _store = store;
    }

    public async Task<IUserNameReadModel?> ExecuteQueryAsync(GetUserNameByNameQuery query,
        CancellationToken cancellationToken)
    {
        var item = await _store.GetAsync(UserNameId.Create(query.Name).Value, cancellationToken);
        return item.ReadModel;
        //var cursor = await _store.FindAsync(p => p.UserName == query.Name);
        //return await cursor.ToListAsync(cancellationToken);
    }
}
