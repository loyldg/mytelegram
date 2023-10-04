// ReSharper disable All

using MyTelegram.Schema.Messages;
using IStickerSet = MyTelegram.Schema.IStickerSet;

namespace MyTelegram.Handlers.Messages;

public class GetEmojiStickersHandler :
    RpcResultObjectHandler<Schema.Messages.RequestGetEmojiStickers, Schema.Messages.IAllStickers>,
    Messages.IGetEmojiStickersHandler, IProcessedHandler
{
    protected override Task<Schema.Messages.IAllStickers> HandleCoreAsync(IRequestInput input,
        Schema.Messages.RequestGetEmojiStickers obj)
    {
        return Task.FromResult<Schema.Messages.IAllStickers>(new TAllStickers
        {
            Sets = new TVector<IStickerSet>()
        });
    }
}