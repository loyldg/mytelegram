using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTelegram.Domain.Extensions;

namespace MyTelegram.QueryHandlers.MongoDB.Photo;

public class GetPhotosByUserIdQueryHandler : IQueryHandler<GetPhotosByUserIdQuery, IReadOnlyCollection<IPhotoReadModel>>
{
    private readonly IMyMongoDbReadModelStore<PhotoReadModel> _store;

    public GetPhotosByUserIdQueryHandler(IMyMongoDbReadModelStore<PhotoReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IPhotoReadModel>> ExecuteQueryAsync(GetPhotosByUserIdQuery query, CancellationToken cancellationToken)
    {
        var cursor = await _store.FindAsync(p => p.UserId == query.UserId && query.PhotoIds.Contains(p.PhotoId), cancellationToken: cancellationToken);

        return await cursor.ToListAsync(cancellationToken);
    }
}