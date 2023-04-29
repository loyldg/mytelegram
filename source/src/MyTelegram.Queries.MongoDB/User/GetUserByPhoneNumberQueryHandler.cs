namespace MyTelegram.QueryHandlers.MongoDB.User;

// ReSharper disable once UnusedMember.Global
public class GetUserByPhoneNumberQueryHandler : IQueryHandler<GetUserByPhoneNumberQuery, IUserReadModel?>
{
    private readonly IMongoDbReadModelStore<UserReadModel> _store;

    public GetUserByPhoneNumberQueryHandler(IMongoDbReadModelStore<UserReadModel> store)
    {
        _store = store;
    }

    public async Task<IUserReadModel?> ExecuteQueryAsync(GetUserByPhoneNumberQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store
                .FindAsync(p => p.PhoneNumber == query.PhoneNumber, cancellationToken: cancellationToken)
            ;
        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }
}
