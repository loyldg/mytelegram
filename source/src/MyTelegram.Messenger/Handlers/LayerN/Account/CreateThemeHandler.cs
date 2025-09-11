namespace MyTelegram.Messenger.Handlers.LatestLayer.LayerN.Account;

///<summary>
/// Create a theme
/// <para>Possible errors</para>
/// Code Type Description
/// 400 THEME_MIME_INVALID The theme's MIME type is invalid.
/// 400 THEME_TITLE_INVALID The specified theme title is invalid.
/// See <a href="https://corefork.telegram.org/method/account.createTheme" />
///</summary>
internal sealed class CreateThemeHandler(
    IHandlerHelper handlerHelper,
    IRequestConverter<MyTelegram.Schema.Account.LayerN.RequestCreateTheme,
        MyTelegram.Schema.Account.RequestCreateTheme> dataConverter)
    : ForwardRequestToNewHandler<
            MyTelegram.Schema.Account.LayerN.RequestCreateTheme,
            MyTelegram.Schema.Account.RequestCreateTheme
        >(handlerHelper, dataConverter), IDistinctObjectHandler
{
}