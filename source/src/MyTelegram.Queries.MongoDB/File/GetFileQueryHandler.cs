//namespace MyTelegram.QueryHandlers.MongoDB.File;

//public class GetFileQueryHandler : IQueryHandler<GetFileQuery, IFileReadModel?>
//{
//    private readonly IMongoDbReadModelStore<FileReadModel> _store;

//    public GetFileQueryHandler(IMongoDbReadModelStore<FileReadModel> store)
//    {
//        _store = store;
//    }

//    public async Task<IFileReadModel?> ExecuteQueryAsync(GetFileQuery query,
//        CancellationToken cancellationToken)
//    {
//        var cursor = await _store.FindAsync(p =>
//                p.ServerFileId == query.FileId || p.FileId == query.FileId ||
//                p.FileReference == query.FileReference,
//            cancellationToken: cancellationToken).ConfigureAwait(false);
//        return await cursor.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
//    }
//}


