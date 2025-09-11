namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Get sensitive content settings
/// See <a href="https://corefork.telegram.org/method/account.getContentSettings" />
///</summary>
internal sealed class GetContentSettingsHandler(IUserAppService userAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetContentSettings,
            MyTelegram.Schema.Account.IContentSettings>
{
    protected override async Task<MyTelegram.Schema.Account.IContentSettings> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetContentSettings obj)
    {
        var user = await userAppService.GetAsync(input.UserId);

        return new TContentSettings
        {
            SensitiveCanChange = user!.SensitiveCanChange,
            SensitiveEnabled = user.SensitiveEnabled
        };
    }
}
