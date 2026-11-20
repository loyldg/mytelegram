namespace MyTelegram.QueryHandlers.MongoDB.Dialog;

public class GetDialogsQueryHandler(IQueryOnlyReadModelStore<DialogReadModel> store) : IQueryHandler<GetDialogsQuery, IReadOnlyCollection<IDialogReadModel>>
{
    public async Task<IReadOnlyCollection<IDialogReadModel>> ExecuteQueryAsync(GetDialogsQuery query,
        CancellationToken cancellationToken)
    {
        // Fix native aot mission metadata issues
        var needPinnedParameter = false;
        var needOffsetDate = false;
        var pinned = false;
        int? offsetDate = null;
        if (query.Pinned.HasValue)
        {
            needPinnedParameter = true;
            pinned = query.Pinned.Value;
        }

        if (query.OffsetDate.HasValue)
        {
            needOffsetDate = true;
            offsetDate = query.OffsetDate.Value;
        }

        // Dialogs are sorted by (LastMessageDate, TopMessage) descending, offset_id breaks ties of offset_date
        var offsetTopMessage = query.Offset?.OffsetId ?? 0;

        Expression<Func<DialogReadModel, bool>> predicate = x => x.OwnerId == query.OwnerId &&
                                                                 !x.IsDeleted
                                                                 ;
        predicate = predicate
                //.WhereIf(needOffsetDate, p => p.LastMessageDate < offsetDate || (p.LastMessageDate == offsetDate && p.TopMessage < offsetTopMessage))
                .WhereIf(needPinnedParameter, p => p.Pinned == pinned)
                .WhereIf(query.PeerIdList?.Count > 0, p => query.PeerIdList!.Contains(p.ToPeerId))
                .WhereIf(query.FolderId.HasValue && query.FolderId != 0, p => p.FolderId == query.FolderId)
            ;

        var sort = new SortOptions<DialogReadModel>(p => p.TopMessage, SortType.Descending);
        return await store.FindAsync(predicate, limit: query.Limit, sort: sort, cancellationToken: cancellationToken);
    }
}
