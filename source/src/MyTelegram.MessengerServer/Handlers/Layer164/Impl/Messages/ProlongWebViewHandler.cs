// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class ProlongWebViewHandler : RpcResultObjectHandler<RequestProlongWebView, IBool>,
    Messages.IProlongWebViewHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestProlongWebView obj)
    {
        throw new NotImplementedException();
    }
}