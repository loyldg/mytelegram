namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Change media autodownload settings
/// See <a href="https://corefork.telegram.org/method/account.saveAutoDownloadSettings" />
///</summary>
internal sealed class SaveAutoDownloadSettingsHandler (ILogger<SaveAutoDownloadSettingsHandler> logger): RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSaveAutoDownloadSettings, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSaveAutoDownloadSettings obj)
    {
        logger.LogInformation("SaveAutoDownloadSettingsHandler: {@Data}",obj);
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
