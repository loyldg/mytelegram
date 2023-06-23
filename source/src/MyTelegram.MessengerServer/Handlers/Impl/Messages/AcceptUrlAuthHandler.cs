using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class AcceptUrlAuthHandler : RpcResultObjectHandler<RequestAcceptUrlAuth, IUrlAuthResult>,
    IAcceptUrlAuthHandler
{
    protected override Task<IUrlAuthResult> HandleCoreAsync(IRequestInput input,
        RequestAcceptUrlAuth obj)
    {
        throw new NotImplementedException();
    }
}