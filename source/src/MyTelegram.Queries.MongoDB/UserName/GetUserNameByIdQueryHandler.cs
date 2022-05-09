namespace MyTelegram.QueryHandlers.MongoDB.UserName;
// ReSharper disable once UnusedMember.Global
public class GetUserNameByIdQueryHandler : IQueryHandler<GetUserNameByIdQuery, IUserNameReadModel?>
{
    private readonly IMongoDbReadModelStore<UserNameReadModel> _store;

    public GetUserNameByIdQueryHandler(IMongoDbReadModelStore<UserNameReadModel> store)
    {
        _store = store;
    }

    public async Task<IUserNameReadModel?> ExecuteQueryAsync(GetUserNameByIdQuery query,
        CancellationToken cancellationToken)
    {
        var item = await _store
            .GetAsync(UserNameId.Create(query.UserName).Value, cancellationToken)
            .ConfigureAwait(false);

        return item.ReadModel;
    }
}
