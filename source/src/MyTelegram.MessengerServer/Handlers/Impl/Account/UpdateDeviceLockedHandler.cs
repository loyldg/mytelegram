using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class UpdateDeviceLockedHandler : RpcResultObjectHandler<RequestUpdateDeviceLocked, IBool>,
    IUpdateDeviceLockedHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUpdateDeviceLocked obj)
    {
        throw new NotImplementedException();
    }
}
