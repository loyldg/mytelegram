using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ReceivedQueueHandler : RpcResultObjectHandler<RequestReceivedQueue, TVector<long>>,
    IReceivedQueueHandler
{
    protected override Task<TVector<long>> HandleCoreAsync(IRequestInput input,
        RequestReceivedQueue obj)
    {
        throw new NotImplementedException();
    }
}