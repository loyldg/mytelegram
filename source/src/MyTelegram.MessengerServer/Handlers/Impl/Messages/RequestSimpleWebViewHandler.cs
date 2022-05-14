// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class RequestSimpleWebViewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRequestSimpleWebView, MyTelegram.Schema.ISimpleWebViewResult>,
    Messages.IRequestSimpleWebViewHandler
{
    protected override Task<MyTelegram.Schema.ISimpleWebViewResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestRequestSimpleWebView obj)
    {
        throw new NotImplementedException();
    }
}
