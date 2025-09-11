namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get all installed stickers
/// See <a href="https://corefork.telegram.org/method/messages.getAllStickers" />
///</summary>
internal sealed class GetAllStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAllStickers, MyTelegram.Schema.Messages.IAllStickers>
{
    protected override Task<MyTelegram.Schema.Messages.IAllStickers> HandleCoreAsync(IRequestInput input,
        RequestGetAllStickers obj)
    {
        var r = new MyTelegram.Schema.Messages.TAllStickers { Sets = [] };

        return Task.FromResult<IAllStickers>(r);
    }
}
