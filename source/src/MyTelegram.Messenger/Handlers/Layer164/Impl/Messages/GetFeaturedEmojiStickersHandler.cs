// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Gets featured custom emoji stickersets.
/// See <a href="https://corefork.telegram.org/method/messages.getFeaturedEmojiStickers" />
///</summary>
internal sealed class GetFeaturedEmojiStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetFeaturedEmojiStickers, MyTelegram.Schema.Messages.IFeaturedStickers>,
    Messages.IGetFeaturedEmojiStickersHandler
{
    protected override Task<MyTelegram.Schema.Messages.IFeaturedStickers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetFeaturedEmojiStickers obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IFeaturedStickers>(new TFeaturedStickers
        {
            Sets = new(),
            Unread = new()
        });
    }
}
