// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetFeaturedEmojiStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetFeaturedEmojiStickers, MyTelegram.Schema.Messages.IFeaturedStickers>,
    Messages.IGetFeaturedEmojiStickersHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.Messages.IFeaturedStickers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetFeaturedEmojiStickers obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IFeaturedStickers>(new TFeaturedStickersNotModified());
    }
}
