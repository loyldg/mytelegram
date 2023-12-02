namespace MyTelegram.QueryHandlers.MongoDB.Photo;

public class
    GetPhotosByPhotoIdLisQueryHandler : IQueryHandler<GetPhotosByPhotoIdLisQuery, IReadOnlyCollection<IPhotoReadModel>>
{
    private readonly IMyMongoDbReadModelStore<PhotoReadModel> _store;

    public GetPhotosByPhotoIdLisQueryHandler(IMyMongoDbReadModelStore<PhotoReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IPhotoReadModel>> ExecuteQueryAsync(GetPhotosByPhotoIdLisQuery query, CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => query.PhotoIds.Contains(p.PhotoId), cancellationToken: cancellationToken);

        return await cursor.ToListAsync(cancellationToken);
    }
}