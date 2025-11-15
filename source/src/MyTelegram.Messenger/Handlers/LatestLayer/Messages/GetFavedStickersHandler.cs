namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get faved stickers
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getFavedStickers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetFavedStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetFavedStickers, MyTelegram.Schema.Messages.IFavedStickers>
{
    protected override Task<MyTelegram.Schema.Messages.IFavedStickers> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetFavedStickers obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IFavedStickers>(new TFavedStickers { Packs = new(), Stickers = new(), });
    }
}