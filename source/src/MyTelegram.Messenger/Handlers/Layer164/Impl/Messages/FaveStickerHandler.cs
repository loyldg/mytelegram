// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Mark or unmark a sticker as favorite
/// <para>Possible errors</para>
/// Code Type Description
/// 400 STICKER_ID_INVALID The provided sticker ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.faveSticker" />
///</summary>
internal sealed class FaveStickerHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestFaveSticker, IBool>,
    Messages.IFaveStickerHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestFaveSticker obj)
    {
        throw new NotImplementedException();
    }
}
