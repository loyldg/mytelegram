// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Search for stickersets
/// See <a href="https://corefork.telegram.org/method/messages.searchStickerSets" />
///</summary>
internal sealed class SearchStickerSetsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearchStickerSets, MyTelegram.Schema.Messages.IFoundStickerSets>,
    Messages.ISearchStickerSetsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IFoundStickerSets> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSearchStickerSets obj)
    {
        throw new NotImplementedException();
    }
}
