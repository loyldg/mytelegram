using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetSplitRangesHandler : RpcResultObjectHandler<RequestGetSplitRanges, TVector<IMessageRange>>,
    IGetSplitRangesHandler
{
    protected override Task<TVector<IMessageRange>> HandleCoreAsync(IRequestInput input,
        RequestGetSplitRanges obj)
    {
        throw new NotImplementedException();
    }
}