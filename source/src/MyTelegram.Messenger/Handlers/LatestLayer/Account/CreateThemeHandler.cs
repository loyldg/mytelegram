namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Create a theme
/// Possible errors
/// Code Type Description
/// 400 THEME_MIME_INVALID The theme's MIME type is invalid.
/// 400 THEME_TITLE_INVALID The specified theme title is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.createTheme"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class CreateThemeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestCreateTheme, MyTelegram.Schema.ITheme>
{
    protected override Task<MyTelegram.Schema.ITheme> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestCreateTheme obj)
    {
        throw new NotImplementedException();
    }
}