namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Set sensitive content settings (for viewing or hiding NSFW content)
/// Possible errors
/// Code Type Description
/// 403 SENSITIVE_CHANGE_FORBIDDEN You can't change your sensitive content settings.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.setContentSettings"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SetContentSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSetContentSettings, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestSetContentSettings obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}