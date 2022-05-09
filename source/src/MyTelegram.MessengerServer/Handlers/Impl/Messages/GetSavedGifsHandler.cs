using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetSavedGifsHandler : RpcResultObjectHandler<RequestGetSavedGifs, ISavedGifs>,
    IGetSavedGifsHandler, IProcessedHandler
{
    protected override Task<ISavedGifs> HandleCoreAsync(IRequestInput input,
        RequestGetSavedGifs obj)
    {
        return Task.FromResult<ISavedGifs>(new TSavedGifs { Gifs = new TVector<IDocument>() });
    }
}
