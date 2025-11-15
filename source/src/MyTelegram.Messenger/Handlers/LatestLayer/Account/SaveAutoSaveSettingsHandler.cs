namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Modify autosave settings
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.saveAutoSaveSettings"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SaveAutoSaveSettingsHandler(ILogger<SaveAutoSaveSettingsHandler> logger) : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSaveAutoSaveSettings, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestSaveAutoSaveSettings obj)
    {
        logger.LogInformation("SaveAutoSaveSettingsHandler: {@Data}", obj);
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}