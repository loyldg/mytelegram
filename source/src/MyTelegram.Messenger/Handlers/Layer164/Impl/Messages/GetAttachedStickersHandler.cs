// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get stickers attached to a photo or video
/// See <a href="https://corefork.telegram.org/method/messages.getAttachedStickers" />
///</summary>
internal sealed class GetAttachedStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAttachedStickers, TVector<MyTelegram.Schema.IStickerSetCovered>>,
    Messages.IGetAttachedStickersHandler
{
    protected override Task<TVector<MyTelegram.Schema.IStickerSetCovered>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetAttachedStickers obj)
    {
        throw new NotImplementedException();
    }
}
