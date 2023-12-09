// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Indicate to the server (from the user side) that the user is still using a web app.If the method returns a <code>QUERY_ID_INVALID</code> error, the webview must be closed.
/// See <a href="https://corefork.telegram.org/method/messages.prolongWebView" />
///</summary>
internal sealed class ProlongWebViewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestProlongWebView, IBool>,
    Messages.IProlongWebViewHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestProlongWebView obj)
    {
        throw new NotImplementedException();
    }
}
