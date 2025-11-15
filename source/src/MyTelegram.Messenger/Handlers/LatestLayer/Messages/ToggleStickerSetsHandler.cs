namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Apply changes to multiple stickersets
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.toggleStickerSets"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleStickerSetsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleStickerSets, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestToggleStickerSets obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}