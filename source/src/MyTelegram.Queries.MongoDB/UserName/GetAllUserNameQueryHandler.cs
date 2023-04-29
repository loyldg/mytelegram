namespace MyTelegram.QueryHandlers.MongoDB.UserName;
// ReSharper disable once UnusedMember.Global
public class GetAllUserNameQueryHandler : IQueryHandler<GetAllUserNameQuery, IReadOnlyCollection<string>>
{
    private readonly IMyMongoDbReadModelStore<UserNameReadModel> _store;

    public GetAllUserNameQueryHandler(IMyMongoDbReadModelStore<UserNameReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<string>> ExecuteQueryAsync(GetAllUserNameQuery query,
        CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<UserNameReadModel, string> {
            Limit = query.Limit,
            Skip = query.Skip,
            Projection = new ProjectionDefinitionBuilder<UserNameReadModel>().Expression(p => p.UserName)
        };

        var cursor = await _store.FindAsync(p => true, findOptions, cancellationToken);

        return await cursor.ToListAsync(cancellationToken);
    }
}
