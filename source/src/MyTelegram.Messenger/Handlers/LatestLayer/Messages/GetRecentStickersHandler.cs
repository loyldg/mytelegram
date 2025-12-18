namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get recent stickers
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getRecentStickers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetRecentStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetRecentStickers, MyTelegram.Schema.Messages.IRecentStickers>
{
    protected override Task<MyTelegram.Schema.Messages.IRecentStickers> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetRecentStickers obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IRecentStickers>(new TRecentStickers { Dates = [], Packs = [], Stickers = [], });
    }
}