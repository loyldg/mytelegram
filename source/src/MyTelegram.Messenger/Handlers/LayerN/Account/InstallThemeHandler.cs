namespace MyTelegram.Messenger.Handlers.LatestLayer.LayerN.Account;

///<summary>
/// Install a theme
/// See <a href="https://corefork.telegram.org/method/account.installTheme" />
///</summary>
internal sealed class InstallThemeHandler(
    IHandlerHelper handlerHelper,
    IRequestConverter<MyTelegram.Schema.Account.LayerN.RequestInstallTheme,
        MyTelegram.Schema.Account.RequestInstallTheme> dataConverter)
    : ForwardRequestToNewHandler<
            MyTelegram.Schema.Account.LayerN.RequestInstallTheme,
            MyTelegram.Schema.Account.RequestInstallTheme
        >(handlerHelper, dataConverter), IDistinctObjectHandler
{
}