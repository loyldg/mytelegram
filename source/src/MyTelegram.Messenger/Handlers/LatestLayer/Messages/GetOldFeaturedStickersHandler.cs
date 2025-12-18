namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Method for fetching previously featured stickers
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getOldFeaturedStickers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetOldFeaturedStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetOldFeaturedStickers, MyTelegram.Schema.Messages.IFeaturedStickers>
{
    protected override Task<MyTelegram.Schema.Messages.IFeaturedStickers> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetOldFeaturedStickers obj)
    {
        throw new NotImplementedException();
    }
}