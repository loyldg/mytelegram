namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Resets all notification settings from users and groups.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.resetNotifySettings"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ResetNotifySettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestResetNotifySettings, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestResetNotifySettings obj)
    {
        // TODO: ResetNotifySettingsHandler
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}