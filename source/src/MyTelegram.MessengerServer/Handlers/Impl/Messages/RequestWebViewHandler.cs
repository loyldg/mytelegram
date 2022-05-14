// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class RequestWebViewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRequestWebView, MyTelegram.Schema.IWebViewResult>,
    Messages.IRequestWebViewHandler
{
    protected override Task<MyTelegram.Schema.IWebViewResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestRequestWebView obj)
    {
        throw new NotImplementedException();
    }
}
