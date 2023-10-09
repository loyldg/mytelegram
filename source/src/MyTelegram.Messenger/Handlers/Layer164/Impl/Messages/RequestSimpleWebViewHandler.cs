// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Open a <a href="https://corefork.telegram.org/api/bots/webapps">bot web app</a>.
/// See <a href="https://corefork.telegram.org/method/messages.requestSimpleWebView" />
///</summary>
internal sealed class RequestSimpleWebViewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRequestSimpleWebView, MyTelegram.Schema.ISimpleWebViewResult>,
    Messages.IRequestSimpleWebViewHandler
{
    protected override Task<MyTelegram.Schema.ISimpleWebViewResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestRequestSimpleWebView obj)
    {
        throw new NotImplementedException();
    }
}
