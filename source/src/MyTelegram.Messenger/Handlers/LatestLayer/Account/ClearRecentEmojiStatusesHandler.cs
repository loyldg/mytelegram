namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Clears list of recently used <a href="https://corefork.telegram.org/api/emoji-status">emoji statuses</a>
/// <para><c>See <a href="https://corefork.telegram.org/method/account.clearRecentEmojiStatuses"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ClearRecentEmojiStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestClearRecentEmojiStatuses, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestClearRecentEmojiStatuses obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}