namespace MyTelegram.QueryHandlers.MongoDB.User;

public class GetGlobalPrivacySettingsListQueryHandler : IQueryHandler<GetGlobalPrivacySettingsListQuery,
    IReadOnlyDictionary<long, GlobalPrivacySettings?>>
{
    private readonly IMyMongoDbReadModelStore<UserReadModel> _store;

    public GetGlobalPrivacySettingsListQueryHandler(IMyMongoDbReadModelStore<UserReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyDictionary<long, GlobalPrivacySettings?>> ExecuteQueryAsync(GetGlobalPrivacySettingsListQuery query, CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<UserReadModel, GlobalPrivacySettingsWithUserId>
        {
            Projection = Builders<UserReadModel>.Projection.Expression(p => new GlobalPrivacySettingsWithUserId(p.UserId, p.GlobalPrivacySettings))
        };
        var cursor = await _store.FindAsync(p => query.UserIds.Contains(p.UserId), findOptions, cancellationToken);

        return (await cursor.ToListAsync(cancellationToken: cancellationToken)).ToDictionary(k => k.UserId, v => v.GlobalPrivacySettings);
    }

    public record GlobalPrivacySettingsWithUserId(long UserId, GlobalPrivacySettings? GlobalPrivacySettings);
}

