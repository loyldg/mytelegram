// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get featured stickers
/// See <a href="https://corefork.telegram.org/method/messages.getFeaturedStickers" />
///</summary>
internal sealed class GetFeaturedStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetFeaturedStickers, MyTelegram.Schema.Messages.IFeaturedStickers>,
    Messages.IGetFeaturedStickersHandler
{
    protected override Task<MyTelegram.Schema.Messages.IFeaturedStickers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetFeaturedStickers obj)
    {
        throw new NotImplementedException();
    }
}
