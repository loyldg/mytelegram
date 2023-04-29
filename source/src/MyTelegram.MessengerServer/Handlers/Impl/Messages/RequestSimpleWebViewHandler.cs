// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class RequestSimpleWebViewHandler :
    RpcResultObjectHandler<RequestRequestSimpleWebView, Schema.ISimpleWebViewResult>,
    Messages.IRequestSimpleWebViewHandler
{
    protected override Task<Schema.ISimpleWebViewResult> HandleCoreAsync(IRequestInput input,
        RequestRequestSimpleWebView obj)
    {
        throw new NotImplementedException();
    }
}
