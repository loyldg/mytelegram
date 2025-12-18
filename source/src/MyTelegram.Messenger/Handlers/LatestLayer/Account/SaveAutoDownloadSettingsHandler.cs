namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Change media autodownload settings
/// <para><c>See <a href="https://corefork.telegram.org/method/account.saveAutoDownloadSettings"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SaveAutoDownloadSettingsHandler(ILogger<SaveAutoDownloadSettingsHandler> logger) : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSaveAutoDownloadSettings, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestSaveAutoDownloadSettings obj)
    {
        logger.LogInformation("SaveAutoDownloadSettingsHandler: {@Data}", obj);
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}