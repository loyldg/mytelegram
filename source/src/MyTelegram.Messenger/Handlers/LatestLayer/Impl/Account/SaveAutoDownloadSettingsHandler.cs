// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Change media autodownload settings
/// See <a href="https://corefork.telegram.org/method/account.saveAutoDownloadSettings" />
///</summary>
internal sealed class SaveAutoDownloadSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSaveAutoDownloadSettings, IBool>,
    Account.ISaveAutoDownloadSettingsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSaveAutoDownloadSettings obj)
    {
        throw new NotImplementedException();
    }
}
