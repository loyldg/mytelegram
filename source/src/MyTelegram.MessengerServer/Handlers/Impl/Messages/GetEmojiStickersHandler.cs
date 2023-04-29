// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetEmojiStickersHandler :
    RpcResultObjectHandler<Schema.Messages.RequestGetEmojiStickers, Schema.Messages.IAllStickers>,
    Messages.IGetEmojiStickersHandler, IProcessedHandler
{
    protected override Task<Schema.Messages.IAllStickers> HandleCoreAsync(IRequestInput input,
        Schema.Messages.RequestGetEmojiStickers obj)
    {
        return Task.FromResult<Schema.Messages.IAllStickers>(new TAllStickersNotModified());
    }
}
