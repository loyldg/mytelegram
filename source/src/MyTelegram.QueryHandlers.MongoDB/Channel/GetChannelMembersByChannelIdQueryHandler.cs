namespace MyTelegram.QueryHandlers.MongoDB.Channel;

public class GetChannelMembersByChannelIdQueryHandler(IQueryOnlyReadModelStore<ChannelMemberReadModel> store) :
    IQueryHandler<GetChannelMembersByChannelIdQuery,
        IReadOnlyCollection<IChannelMemberReadModel>>
{
    public async Task<IReadOnlyCollection<IChannelMemberReadModel>> ExecuteQueryAsync(
        GetChannelMembersByChannelIdQuery query,
        CancellationToken cancellationToken)
    {
        Expression<Func<ChannelMemberReadModel, bool>> filter = p => /*!p.Left && !p.Kicked && */p.ChannelId == query.ChannelId;
        filter = filter.WhereIf(query.MemberUserIdList.Count > 0, p => query.MemberUserIdList.Contains(p.UserId))
                .WhereIf(query.OnlyAdmin, p => p.IsAdmin)
                .WhereIf(query.OnlyBots, p => p.IsBot)
                .WhereIf(query.OnlyKicked, p => p.Kicked)
                .WhereIf(!query.OnlyKicked, p => !p.Left && !p.Kicked)
                .WhereIf(query.OnlyBanned, p => p.BannedRights != 0)
            ;

        return await store.FindAsync(filter, skip: query.Offset, limit: query.Limit, cancellationToken: cancellationToken);
    }
}