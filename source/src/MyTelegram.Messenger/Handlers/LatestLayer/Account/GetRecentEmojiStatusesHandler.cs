namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Get recently used <a href="https://corefork.telegram.org/api/emoji-status">emoji statuses</a>
/// See <a href="https://corefork.telegram.org/method/account.getRecentEmojiStatuses" />
///</summary>
internal sealed class GetRecentEmojiStatusesHandler(IUserAppService userAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetRecentEmojiStatuses,
            MyTelegram.Schema.Account.IEmojiStatuses>
{
    protected override async Task<MyTelegram.Schema.Account.IEmojiStatuses> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetRecentEmojiStatuses obj)
    {
        if (input.UserId == 0)
        {
            return new TEmojiStatuses
            {
                Statuses = []
            };
        }

        var user = await userAppService.GetAsync(input.UserId);
        if (user == null)
        {
            RpcErrors.RpcErrors400.PeerIdInvalid.ThrowRpcError();
        }

        if (user!.RecentEmojiStatuses?.Count > 0)
        {
            return new TEmojiStatuses
            {
                Statuses = [.. user!.RecentEmojiStatuses.Select(p => new TEmojiStatus()
                {
                    DocumentId = p
                })]
            };
        }

        return new TEmojiStatuses
        {
            Statuses = []
        };
    }
}
