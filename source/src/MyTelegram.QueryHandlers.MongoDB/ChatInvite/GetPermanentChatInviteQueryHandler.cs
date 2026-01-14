namespace MyTelegram.QueryHandlers.MongoDB.ChatInvite;

public class GetPermanentChatInviteQueryHandler(IQueryOnlyReadModelStore<ChatInviteReadModel> store) : IQueryHandler<GetPermanentChatInviteQuery, IChatInviteReadModel?>
{
    public async Task<IChatInviteReadModel?> ExecuteQueryAsync(GetPermanentChatInviteQuery query, CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p => p.PeerId == query.ChannelId && p.Permanent && !p.Revoked && p.AdminId == query.AdminUserId, cancellationToken: cancellationToken);
    }
}