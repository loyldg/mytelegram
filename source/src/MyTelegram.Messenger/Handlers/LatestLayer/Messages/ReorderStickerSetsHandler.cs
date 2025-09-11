namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Reorder installed stickersets
/// See <a href="https://corefork.telegram.org/method/messages.reorderStickerSets" />
///</summary>
internal sealed class ReorderStickerSetsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReorderStickerSets, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReorderStickerSets obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
