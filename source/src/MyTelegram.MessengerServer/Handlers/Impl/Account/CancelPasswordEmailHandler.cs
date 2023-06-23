using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

// ReSharper disable once UnusedMember.Global
public class CancelPasswordEmailHandler : RpcResultObjectHandler<RequestCancelPasswordEmail, IBool>,
    ICancelPasswordEmailHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestCancelPasswordEmail obj)
    {
        throw new NotImplementedException();
    }
}