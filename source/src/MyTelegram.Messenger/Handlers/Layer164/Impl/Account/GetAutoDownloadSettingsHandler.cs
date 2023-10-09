// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get media autodownload settings
/// See <a href="https://corefork.telegram.org/method/account.getAutoDownloadSettings" />
///</summary>
internal sealed class GetAutoDownloadSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetAutoDownloadSettings, MyTelegram.Schema.Account.IAutoDownloadSettings>,
    Account.IGetAutoDownloadSettingsHandler
{
    protected override Task<MyTelegram.Schema.Account.IAutoDownloadSettings> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetAutoDownloadSettings obj)
    {
        throw new NotImplementedException();
    }
}
