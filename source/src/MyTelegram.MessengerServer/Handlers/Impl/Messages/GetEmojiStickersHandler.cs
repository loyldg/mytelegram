// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetEmojiStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiStickers, MyTelegram.Schema.Messages.IAllStickers>,
    Messages.IGetEmojiStickersHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAllStickers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetEmojiStickers obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IAllStickers>(new TAllStickersNotModified());
    }
}
