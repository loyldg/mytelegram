namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.searchStickers" />
///</summary>
internal sealed class SearchStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearchStickers, MyTelegram.Schema.Messages.IFoundStickers>
{
    protected override Task<MyTelegram.Schema.Messages.IFoundStickers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSearchStickers obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IFoundStickers>(new TFoundStickers
        {
            Stickers = []
        });
    }
}
