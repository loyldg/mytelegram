namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Get the current <a href="https://corefork.telegram.org/api/reactions#notifications-about-reactions">reaction notification settings »</a>.
/// See <a href="https://corefork.telegram.org/method/account.getReactionsNotifySettings" />
///</summary>
internal sealed class GetReactionsNotifySettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetReactionsNotifySettings, MyTelegram.Schema.IReactionsNotifySettings>
{
    protected override Task<MyTelegram.Schema.IReactionsNotifySettings> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetReactionsNotifySettings obj)
    {
        return System.Threading.Tasks.Task.FromResult<MyTelegram.Schema.IReactionsNotifySettings>(
            new TReactionsNotifySettings
            {
                ShowPreviews = true,
                Sound = new TNotificationSoundDefault()
            });
    }
}
