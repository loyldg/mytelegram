using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class RequestUrlAuthHandler : RpcResultObjectHandler<RequestRequestUrlAuth, IUrlAuthResult>,
    IRequestUrlAuthHandler
{
    protected override Task<IUrlAuthResult> HandleCoreAsync(IRequestInput input,
        RequestRequestUrlAuth obj)
    {
        throw new NotImplementedException();
    }
}