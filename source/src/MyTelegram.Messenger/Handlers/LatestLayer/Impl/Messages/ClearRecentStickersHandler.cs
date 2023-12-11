// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Clear recent stickers
/// See <a href="https://corefork.telegram.org/method/messages.clearRecentStickers" />
///</summary>
internal sealed class ClearRecentStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestClearRecentStickers, IBool>,
    Messages.IClearRecentStickersHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestClearRecentStickers obj)
    {
        throw new NotImplementedException();
    }
}
