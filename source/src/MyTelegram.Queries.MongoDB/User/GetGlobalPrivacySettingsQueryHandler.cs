namespace MyTelegram.QueryHandlers.MongoDB.User;

public class GetGlobalPrivacySettingsQueryHandler : IQueryHandler<GetGlobalPrivacySettingsQuery, GlobalPrivacySettings?>
{
    private readonly IMyMongoDbReadModelStore<UserReadModel> _store;

    public GetGlobalPrivacySettingsQueryHandler(IMyMongoDbReadModelStore<UserReadModel> store)
    {
        _store = store;
    }

    public async Task<GlobalPrivacySettings?> ExecuteQueryAsync(GetGlobalPrivacySettingsQuery query, CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<UserReadModel, GlobalPrivacySettings?>
        {
            Projection = Builders<UserReadModel>.Projection.Expression(p => p.GlobalPrivacySettings)
        };
        var cursor = await _store.FindAsync(p => p.UserId == query.UserId, findOptions, cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}