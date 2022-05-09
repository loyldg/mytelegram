using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetStickersHandler : RpcResultObjectHandler<RequestGetStickers, IStickers>,
    IGetStickersHandler, IProcessedHandler
{
    protected override Task<IStickers> HandleCoreAsync(IRequestInput input,
        RequestGetStickers obj)
    {
        var r = new TStickers { Hash = obj.Hash, Stickers = new TVector<IDocument>() };

        return Task.FromResult<IStickers>(r);
    }
}
