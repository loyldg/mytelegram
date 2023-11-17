// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get faved stickers
/// See <a href="https://corefork.telegram.org/method/messages.getFavedStickers" />
///</summary>
internal sealed class GetFavedStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetFavedStickers, MyTelegram.Schema.Messages.IFavedStickers>,
    Messages.IGetFavedStickersHandler
{
    protected override Task<MyTelegram.Schema.Messages.IFavedStickers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetFavedStickers obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IFavedStickers>(new TFavedStickers
        {
            Packs = new(),
            Stickers = new(),
        });
    }
}
