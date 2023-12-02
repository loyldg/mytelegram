namespace MyTelegram.QueryHandlers.MongoDB.Photo;

public class GetPhotoByIdQueryHandler : IQueryHandler<GetPhotoByIdQuery, IPhotoReadModel?>
{
    private readonly IMyMongoDbReadModelStore<PhotoReadModel> _store;

    public GetPhotoByIdQueryHandler(IMyMongoDbReadModelStore<PhotoReadModel> store)
    {
        _store = store;
    }

    public async Task<IPhotoReadModel?> ExecuteQueryAsync(GetPhotoByIdQuery query, CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.PhotoId == query.PhotoId, cancellationToken: cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }
}