using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ReceivedMessagesHandler : RpcResultObjectHandler<RequestReceivedMessages, TVector<IReceivedNotifyMessage>>,
    IReceivedMessagesHandler, IProcessedHandler
{
    protected override Task<TVector<IReceivedNotifyMessage>> HandleCoreAsync(IRequestInput input,
        RequestReceivedMessages obj)
    {
        return Task.FromResult(new TVector<IReceivedNotifyMessage> { new TReceivedNotifyMessage { Id = obj.MaxId } });
    }
}