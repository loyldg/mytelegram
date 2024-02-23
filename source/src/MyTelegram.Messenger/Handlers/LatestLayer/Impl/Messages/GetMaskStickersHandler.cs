// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get installed mask stickers
/// See <a href="https://corefork.telegram.org/method/messages.getMaskStickers" />
///</summary>
internal sealed class GetMaskStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMaskStickers, MyTelegram.Schema.Messages.IAllStickers>,
    Messages.IGetMaskStickersHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAllStickers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetMaskStickers obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IAllStickers>(new TAllStickers
        {
            Sets = new()
        });
    }
}
