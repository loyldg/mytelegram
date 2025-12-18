namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Save a theme
/// Possible errors
/// Code Type Description
/// 400 THEME_INVALID Invalid theme provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.saveTheme"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SaveThemeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSaveTheme, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestSaveTheme obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}