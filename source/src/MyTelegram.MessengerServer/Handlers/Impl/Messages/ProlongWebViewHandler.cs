// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class ProlongWebViewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestProlongWebView, IBool>,
    Messages.IProlongWebViewHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestProlongWebView obj)
    {
        throw new NotImplementedException();
    }
}
