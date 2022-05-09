namespace MyTelegram.QueryHandlers.MongoDB.User;

// ReSharper disable once UnusedMember.Global
public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, IUserReadModel?>
{
    private readonly IMongoDbReadModelStore<UserReadModel> _store;

    public GetUserByIdQueryHandler(IMongoDbReadModelStore<UserReadModel> store)
    {
        _store = store;
    }

    public async Task<IUserReadModel?> ExecuteQueryAsync(GetUserByIdQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store.GetAsync(UserId.Create(query.UserId).Value, cancellationToken)
                .ConfigureAwait(false)
            ;
        return cursor.ReadModel;
    }
}
