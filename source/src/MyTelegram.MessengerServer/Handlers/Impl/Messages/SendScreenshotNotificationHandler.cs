using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SendScreenshotNotificationHandler : RpcResultObjectHandler<RequestSendScreenshotNotification, IUpdates>,
    ISendScreenshotNotificationHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSendScreenshotNotification obj)
    {
        throw new NotImplementedException();
    }
}
