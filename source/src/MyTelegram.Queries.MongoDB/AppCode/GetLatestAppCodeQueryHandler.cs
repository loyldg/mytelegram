namespace MyTelegram.QueryHandlers.MongoDB.AppCode;

public class GetLatestAppCodeQueryHandler : IQueryHandler<GetLatestAppCodeQuery, IAppCodeReadModel>
{
    private readonly IMongoDbReadModelStore<AppCodeReadModel> _store;

    public GetLatestAppCodeQueryHandler(IMongoDbReadModelStore<AppCodeReadModel> store)
    {
        _store = store;
    }

    public async Task<IAppCodeReadModel> ExecuteQueryAsync(GetLatestAppCodeQuery query,
        CancellationToken cancellationToken)
    {
        var cursor = await _store //.AsQueryable()
                .FindAsync(p => p.PhoneNumber == query.PhoneNumber && p.PhoneCodeHash == query.PhoneCodeHash,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false)
            ;

        return await cursor.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
    }
}
