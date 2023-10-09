// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Clears list of recently used <a href="https://corefork.telegram.org/api/emoji-status">emoji statuses</a>
/// See <a href="https://corefork.telegram.org/method/account.clearRecentEmojiStatuses" />
///</summary>
internal sealed class ClearRecentEmojiStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestClearRecentEmojiStatuses, IBool>,
    Account.IClearRecentEmojiStatusesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestClearRecentEmojiStatuses obj)
    {
        throw new NotImplementedException();
    }
}
