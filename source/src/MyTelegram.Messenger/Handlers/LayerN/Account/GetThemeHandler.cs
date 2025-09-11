namespace MyTelegram.Messenger.Handlers.LatestLayer.LayerN.Account;

///<summary>
/// Get theme information
/// <para>Possible errors</para>
/// Code Type Description
/// 400 THEME_FORMAT_INVALID Invalid theme format provided.
/// 400 THEME_INVALID Invalid theme provided.
/// See <a href="https://corefork.telegram.org/method/account.getTheme" />
///</summary>
internal sealed class GetThemeHandler(
    IHandlerHelper handlerHelper,
    IRequestConverter<MyTelegram.Schema.Account.LayerN.RequestGetTheme,
        MyTelegram.Schema.Account.RequestGetTheme> dataConverter)
    : ForwardRequestToNewHandler<
            MyTelegram.Schema.Account.LayerN.RequestGetTheme,
            MyTelegram.Schema.Account.RequestGetTheme
        >(handlerHelper, dataConverter)
{
}