// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class SendWebViewDataHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendWebViewData, MyTelegram.Schema.IUpdates>,
    Messages.ISendWebViewDataHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSendWebViewData obj)
    {
        throw new NotImplementedException();
    }
}
