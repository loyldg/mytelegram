namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Change the <a href="https://corefork.telegram.org/api/reactions#notifications-about-reactions">reaction notification settings »</a>.
/// See <a href="https://corefork.telegram.org/method/account.setReactionsNotifySettings" />
///</summary>
internal sealed class SetReactionsNotifySettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSetReactionsNotifySettings, MyTelegram.Schema.IReactionsNotifySettings>
{
    protected override Task<MyTelegram.Schema.IReactionsNotifySettings> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSetReactionsNotifySettings obj)
    {
        return Task.FromResult<IReactionsNotifySettings>(new MyTelegram.Schema.TReactionsNotifySettings
        {
            ShowPreviews = true,
            Sound = new TNotificationSoundDefault()
        });
    }
}
