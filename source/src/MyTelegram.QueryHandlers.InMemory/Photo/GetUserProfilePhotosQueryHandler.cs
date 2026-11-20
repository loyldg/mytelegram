namespace MyTelegram.QueryHandlers.InMemory.Photo;

public class GetUserProfilePhotosQueryHandler(IQueryOnlyReadModelStore<PhotoReadModel> store)
    : IQueryHandler<GetUserProfilePhotosQuery, IReadOnlyCollection<IPhotoReadModel>>
{
    public async Task<IReadOnlyCollection<IPhotoReadModel>> ExecuteQueryAsync(GetUserProfilePhotosQuery query, CancellationToken cancellationToken)
    {
        return await store.FindAsync(p => p.UserId == query.UserId && p.IsProfilePhoto, skip: query.Offset, limit: query.Limit, cancellationToken: cancellationToken);
    }
}