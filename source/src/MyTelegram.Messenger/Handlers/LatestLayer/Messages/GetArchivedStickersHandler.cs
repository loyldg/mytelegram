namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get all archived stickers
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getArchivedStickers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetArchivedStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetArchivedStickers, MyTelegram.Schema.Messages.IArchivedStickers>
{
    protected override Task<IArchivedStickers> HandleCoreAsync(IRequestInput input, RequestGetArchivedStickers obj)
    {
        var r = new TArchivedStickers
        {
            Sets = []
        };
        return Task.FromResult<IArchivedStickers>(r);
    }
}