namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Search for stickersets
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.searchStickerSets"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SearchStickerSetsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearchStickerSets, MyTelegram.Schema.Messages.IFoundStickerSets>
{
    protected override Task<MyTelegram.Schema.Messages.IFoundStickerSets> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSearchStickerSets obj)
    {
        throw new NotImplementedException();
    }
}