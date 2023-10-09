// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Create a theme
/// <para>Possible errors</para>
/// Code Type Description
/// 400 THEME_MIME_INVALID The theme's MIME type is invalid.
/// 400 THEME_TITLE_INVALID The specified theme title is invalid.
/// See <a href="https://corefork.telegram.org/method/account.createTheme" />
///</summary>
internal sealed class CreateThemeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestCreateTheme, MyTelegram.Schema.ITheme>,
    Account.ICreateThemeHandler
{
    protected override Task<MyTelegram.Schema.ITheme> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestCreateTheme obj)
    {
        throw new NotImplementedException();
    }
}
