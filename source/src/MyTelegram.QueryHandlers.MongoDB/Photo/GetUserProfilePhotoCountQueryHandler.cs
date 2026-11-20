namespace MyTelegram.QueryHandlers.MongoDB.Photo;

public class GetUserProfilePhotoCountQueryHandler(IQueryOnlyReadModelStore<PhotoReadModel> store)
    : IQueryHandler<GetUserProfilePhotoCountQuery, int>
{
    public async Task<int> ExecuteQueryAsync(GetUserProfilePhotoCountQuery query, CancellationToken cancellationToken)
    {
        return (int)(await store.CountAsync(p => p.UserId == query.UserId, cancellationToken));
    }
}