namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Open a <a href="https://corefork.telegram.org/bots/webapps">bot mini app</a> from a <a href="https://corefork.telegram.org/api/links#direct-mini-app-links">direct Mini App deep link</a>, sending over user information after user confirmation.After calling this method, until the user closes the webview, <a href="https://corefork.telegram.org/method/messages.prolongWebView">messages.prolongWebView</a> must be called every 60 seconds.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_APP_INVALID The specified bot app is invalid.
/// 400 BOT_APP_SHORTNAME_INVALID The specified bot app short name is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.requestAppWebView" />
///</summary>
internal sealed class RequestAppWebViewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRequestAppWebView, MyTelegram.Schema.IWebViewResult>
{
    protected override Task<MyTelegram.Schema.IWebViewResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestRequestAppWebView obj)
    {
        throw new NotImplementedException();
    }
}
