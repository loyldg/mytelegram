// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Open a <a href="https://corefork.telegram.org/bots/webapps">bot web app</a>, sending over user information after user confirmation.After calling this method, until the user closes the webview, <a href="https://corefork.telegram.org/method/messages.prolongWebView">messages.prolongWebView</a> must be called every 60 seconds.
/// See <a href="https://corefork.telegram.org/method/messages.requestWebView" />
///</summary>
internal sealed class RequestWebViewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRequestWebView, MyTelegram.Schema.IWebViewResult>,
    Messages.IRequestWebViewHandler
{
    protected override Task<MyTelegram.Schema.IWebViewResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestRequestWebView obj)
    {
        throw new NotImplementedException();
    }
}
