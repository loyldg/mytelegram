namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Clear recent stickers
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.clearRecentStickers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ClearRecentStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestClearRecentStickers, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestClearRecentStickers obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}