// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class SendWebViewResultMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendWebViewResultMessage, MyTelegram.Schema.IWebViewMessageSent>,
    Messages.ISendWebViewResultMessageHandler
{
    protected override Task<MyTelegram.Schema.IWebViewMessageSent> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSendWebViewResultMessage obj)
    {
        throw new NotImplementedException();
    }
}
