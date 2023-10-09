// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get sensitive content settings
/// See <a href="https://corefork.telegram.org/method/account.getContentSettings" />
///</summary>
internal sealed class GetContentSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetContentSettings, MyTelegram.Schema.Account.IContentSettings>,
    Account.IGetContentSettingsHandler
{
    protected override Task<MyTelegram.Schema.Account.IContentSettings> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetContentSettings obj)
    {
        throw new NotImplementedException();
    }
}
