namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Open a <a href="https://corefork.telegram.org/bots/webapps">bot mini app</a>, sending over user information after user confirmation.After calling this method, until the user closes the webview, <a href="https://corefork.telegram.org/method/messages.prolongWebView">messages.prolongWebView</a> must be called every 60 seconds.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// 400 BOT_WEBVIEW_DISABLED A webview cannot be opened in the specified conditions: emitted for example if <code>from_bot_menu</code> or <code>url</code> are set and <code>peer</code> is not the chat with the bot.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 403 PRIVACY_PREMIUM_REQUIRED You need a <a href="https://corefork.telegram.org/api/premium">Telegram Premium subscription</a> to send a message to this user.
/// 400 SEND_AS_PEER_INVALID You can't send messages as the specified peer.
/// 400 YOU_BLOCKED_USER You blocked this user.
/// See <a href="https://corefork.telegram.org/method/messages.requestWebView" />
///</summary>
internal sealed class RequestWebViewHandler(ILayeredService<IWebViewResultUrlResponseConverter> webViewResultsLayeredService) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRequestWebView, MyTelegram.Schema.IWebViewResult>
{
    protected override Task<MyTelegram.Schema.IWebViewResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestRequestWebView obj)
    {
        var r = new TWebViewResultUrl
        {
            Url = obj.Url,
            QueryId = input.ReqMsgId
        };

        return Task.FromResult<MyTelegram.Schema.IWebViewResult>(webViewResultsLayeredService.GetConverter(input.Layer).ToLayeredData(r));
    }
}
