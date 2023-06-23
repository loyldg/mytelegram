using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class SetContactSignUpNotificationHandler : RpcResultObjectHandler<RequestSetContactSignUpNotification, IBool>,
    ISetContactSignUpNotificationHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetContactSignUpNotification obj)
    {
        throw new NotImplementedException();
    }
}