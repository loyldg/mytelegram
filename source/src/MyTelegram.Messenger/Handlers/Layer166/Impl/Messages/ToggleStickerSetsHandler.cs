// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Apply changes to multiple stickersets
/// See <a href="https://corefork.telegram.org/method/messages.toggleStickerSets" />
///</summary>
internal sealed class ToggleStickerSetsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleStickerSets, IBool>,
    Messages.IToggleStickerSetsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestToggleStickerSets obj)
    {
        throw new NotImplementedException();
    }
}
