namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Update theme
/// Possible errors
/// Code Type Description
/// 400 THEME_INVALID Invalid theme provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.updateTheme"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateThemeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateTheme, MyTelegram.Schema.ITheme>
{
    protected override Task<MyTelegram.Schema.ITheme> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestUpdateTheme obj)
    {
        throw new NotImplementedException();
    }
}