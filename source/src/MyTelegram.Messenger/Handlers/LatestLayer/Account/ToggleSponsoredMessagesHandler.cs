namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Disable or re-enable Telegram ads for the current <a href="https://corefork.telegram.org/api/premium">Premium</a> account.Useful for business owners that may want to launch and view their own Telegram ads via the <a href="https://ads.telegram.org/">Telegram ad platform »</a>.
/// See <a href="https://corefork.telegram.org/method/account.toggleSponsoredMessages" />
///</summary>
internal sealed class ToggleSponsoredMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestToggleSponsoredMessages, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestToggleSponsoredMessages obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
