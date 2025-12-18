namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Open a <a href="https://corefork.telegram.org/bots/webapps">bot mini app</a> from a <a href="https://corefork.telegram.org/api/links#direct-mini-app-links">direct Mini App deep link</a>, sending over user information after user confirmation.After calling this method, until the user closes the webview, <a href="https://corefork.telegram.org/method/messages.prolongWebView">messages.prolongWebView</a> must be called every 60 seconds.
/// Possible errors
/// Code Type Description
/// 400 BOT_APP_BOT_INVALID The bot_id passed in the inputBotAppShortName constructor is invalid.
/// 400 BOT_APP_INVALID The specified bot app is invalid.
/// 400 BOT_APP_SHORTNAME_INVALID The specified bot app short name is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 THEME_PARAMS_INVALID The specified <code>theme_params</code> field is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.requestAppWebView"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class RequestAppWebViewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRequestAppWebView, MyTelegram.Schema.IWebViewResult>
{
    protected override Task<MyTelegram.Schema.IWebViewResult> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestRequestAppWebView obj)
    {
        throw new NotImplementedException();
    }
}