namespace MyTelegram.QueryHandlers.MongoDB.ChatInvite;

public class GetAdminInvitesQueryHandler(IQueryOnlyReadModelStore<ChatInviteReadModel> store) : IQueryHandler<GetAdminInvitesQuery, IReadOnlyCollection<AdminWithInvites>>
{
    public async Task<IReadOnlyCollection<AdminWithInvites>> ExecuteQueryAsync(GetAdminInvitesQuery query, CancellationToken cancellationToken)
    {
        var results = await store.GroupByAsync(p => p.PeerId == query.ChannelId, p => p.AdminId,
            r => new AdminWithInvites(r.Key, r.Count(), r.Count(x => x.Revoked)));

        return results;
    }
}