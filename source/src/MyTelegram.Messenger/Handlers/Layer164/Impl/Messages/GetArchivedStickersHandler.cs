// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get all archived stickers
/// See <a href="https://corefork.telegram.org/method/messages.getArchivedStickers" />
///</summary>
internal sealed class GetArchivedStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetArchivedStickers, MyTelegram.Schema.Messages.IArchivedStickers>,
    Messages.IGetArchivedStickersHandler
{
    protected override Task<MyTelegram.Schema.Messages.IArchivedStickers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetArchivedStickers obj)
    {
        throw new NotImplementedException();
    }
}
