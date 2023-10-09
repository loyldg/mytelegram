// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Method for fetching previously featured stickers
/// See <a href="https://corefork.telegram.org/method/messages.getOldFeaturedStickers" />
///</summary>
internal sealed class GetOldFeaturedStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetOldFeaturedStickers, MyTelegram.Schema.Messages.IFeaturedStickers>,
    Messages.IGetOldFeaturedStickersHandler
{
    protected override Task<MyTelegram.Schema.Messages.IFeaturedStickers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetOldFeaturedStickers obj)
    {
        throw new NotImplementedException();
    }
}
