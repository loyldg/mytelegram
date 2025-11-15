namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Search for stickers using AI-powered keyword search
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.searchStickers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SearchStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearchStickers, MyTelegram.Schema.Messages.IFoundStickers>
{
    protected override Task<MyTelegram.Schema.Messages.IFoundStickers> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSearchStickers obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IFoundStickers>(new TFoundStickers { Stickers = [] });
    }
}