// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetFeaturedEmojiStickersHandler : RpcResultObjectHandler<Schema.Messages.RequestGetFeaturedEmojiStickers,
        Schema.Messages.IFeaturedStickers>,
    Messages.IGetFeaturedEmojiStickersHandler, IProcessedHandler
{
    protected override Task<Schema.Messages.IFeaturedStickers> HandleCoreAsync(IRequestInput input,
        Schema.Messages.RequestGetFeaturedEmojiStickers obj)
    {
        return Task.FromResult<Schema.Messages.IFeaturedStickers>(new TFeaturedStickersNotModified());
    }
}
