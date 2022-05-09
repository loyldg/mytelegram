using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SearchStickerSetsHandler : RpcResultObjectHandler<RequestSearchStickerSets, IFoundStickerSets>,
    ISearchStickerSetsHandler
{
    protected override Task<IFoundStickerSets> HandleCoreAsync(IRequestInput input,
        RequestSearchStickerSets obj)
    {
        throw new NotImplementedException();
    }
}
