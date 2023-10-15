// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get all installed stickers
/// See <a href="https://corefork.telegram.org/method/messages.getAllStickers" />
///</summary>
internal sealed class GetAllStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAllStickers, MyTelegram.Schema.Messages.IAllStickers>,
    Messages.IGetAllStickersHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAllStickers> HandleCoreAsync(IRequestInput input,
        RequestGetAllStickers obj)
    {
        var r = new MyTelegram.Schema.Messages.TAllStickers { Sets = new TVector<MyTelegram.Schema.IStickerSet>() };

        return Task.FromResult<IAllStickers>(r);
    }
}
