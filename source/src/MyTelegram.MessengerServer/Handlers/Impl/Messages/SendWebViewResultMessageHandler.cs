// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class SendWebViewResultMessageHandler :
    RpcResultObjectHandler<RequestSendWebViewResultMessage, Schema.IWebViewMessageSent>,
    Messages.ISendWebViewResultMessageHandler
{
    protected override Task<Schema.IWebViewMessageSent> HandleCoreAsync(IRequestInput input,
        RequestSendWebViewResultMessage obj)
    {
        throw new NotImplementedException();
    }
}
