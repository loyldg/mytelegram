// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.searchEmojiStickerSets" />
///</summary>
internal sealed class SearchEmojiStickerSetsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearchEmojiStickerSets, MyTelegram.Schema.Messages.IFoundStickerSets>,
    Messages.ISearchEmojiStickerSetsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IFoundStickerSets> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSearchEmojiStickerSets obj)
    {
        throw new NotImplementedException();
    }
}
