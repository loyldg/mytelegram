// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Reorder installed stickersets
/// See <a href="https://corefork.telegram.org/method/messages.reorderStickerSets" />
///</summary>
internal sealed class ReorderStickerSetsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReorderStickerSets, IBool>,
    Messages.IReorderStickerSetsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReorderStickerSets obj)
    {
        throw new NotImplementedException();
    }
}
