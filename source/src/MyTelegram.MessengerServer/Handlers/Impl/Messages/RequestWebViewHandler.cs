// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class RequestWebViewHandler : RpcResultObjectHandler<RequestRequestWebView, Schema.IWebViewResult>,
    Messages.IRequestWebViewHandler
{
    protected override Task<Schema.IWebViewResult> HandleCoreAsync(IRequestInput input,
        RequestRequestWebView obj)
    {
        throw new NotImplementedException();
    }
}
