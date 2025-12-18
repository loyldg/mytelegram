namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Gets the list of currently installed <a href="https://corefork.telegram.org/api/custom-emoji">custom emoji stickersets</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getEmojiStickers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetEmojiStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiStickers, MyTelegram.Schema.Messages.IAllStickers>
{
    protected override Task<MyTelegram.Schema.Messages.IAllStickers> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetEmojiStickers obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IAllStickers>(new TAllStickers { Sets = [], });
    }
}