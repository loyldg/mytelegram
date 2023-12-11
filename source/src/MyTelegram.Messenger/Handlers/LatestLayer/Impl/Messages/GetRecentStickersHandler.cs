// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get recent stickers
/// See <a href="https://corefork.telegram.org/method/messages.getRecentStickers" />
///</summary>
internal sealed class GetRecentStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetRecentStickers, MyTelegram.Schema.Messages.IRecentStickers>,
    Messages.IGetRecentStickersHandler
{
    protected override Task<MyTelegram.Schema.Messages.IRecentStickers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetRecentStickers obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IRecentStickers>(new TRecentStickers
        {
            Dates = new(),
            Packs = new(),
            Stickers = new(),
        });
    }
}
