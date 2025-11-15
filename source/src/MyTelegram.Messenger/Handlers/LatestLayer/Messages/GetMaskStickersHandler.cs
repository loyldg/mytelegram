namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get installed mask stickers
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getMaskStickers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetMaskStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMaskStickers, MyTelegram.Schema.Messages.IAllStickers>
{
    protected override Task<MyTelegram.Schema.Messages.IAllStickers> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetMaskStickers obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IAllStickers>(new TAllStickers { Sets = [] });
    }
}