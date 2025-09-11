namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Search for <a href="https://corefork.telegram.org/api/custom-emoji">custom emoji stickersets »</a>
/// See <a href="https://corefork.telegram.org/method/messages.searchEmojiStickerSets" />
///</summary>
internal sealed class SearchEmojiStickerSetsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearchEmojiStickerSets, MyTelegram.Schema.Messages.IFoundStickerSets>
{
    protected override Task<MyTelegram.Schema.Messages.IFoundStickerSets> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSearchEmojiStickerSets obj)
    {
        throw new NotImplementedException();
    }
}
