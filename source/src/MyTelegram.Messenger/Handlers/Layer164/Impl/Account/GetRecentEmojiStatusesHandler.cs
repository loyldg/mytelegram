// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get recently used <a href="https://corefork.telegram.org/api/emoji-status">emoji statuses</a>
/// See <a href="https://corefork.telegram.org/method/account.getRecentEmojiStatuses" />
///</summary>
internal sealed class GetRecentEmojiStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetRecentEmojiStatuses, MyTelegram.Schema.Account.IEmojiStatuses>,
    Account.IGetRecentEmojiStatusesHandler
{
    protected override Task<MyTelegram.Schema.Account.IEmojiStatuses> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetRecentEmojiStatuses obj)
    {
        throw new NotImplementedException();
    }
}
