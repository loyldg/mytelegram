// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Mark new featured stickers as read
/// See <a href="https://corefork.telegram.org/method/messages.readFeaturedStickers" />
///</summary>
internal sealed class ReadFeaturedStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadFeaturedStickers, IBool>,
    Messages.IReadFeaturedStickersHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReadFeaturedStickers obj)
    {
        throw new NotImplementedException();
    }
}
