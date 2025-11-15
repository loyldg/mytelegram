namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get featured stickers
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getFeaturedStickers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetFeaturedStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetFeaturedStickers, MyTelegram.Schema.Messages.IFeaturedStickers>
{
    protected override Task<MyTelegram.Schema.Messages.IFeaturedStickers> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetFeaturedStickers obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IFeaturedStickers>(new TFeaturedStickers { Sets = new(), Unread = new(), });
    }
}