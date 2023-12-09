// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get stickers by emoji
/// <para>Possible errors</para>
/// Code Type Description
/// 400 EMOTICON_EMPTY The emoji is empty.
/// See <a href="https://corefork.telegram.org/method/messages.getStickers" />
///</summary>
internal sealed class GetStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetStickers, MyTelegram.Schema.Messages.IStickers>,
    Messages.IGetStickersHandler
{
    protected override Task<MyTelegram.Schema.Messages.IStickers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetStickers obj)
    {
        var r = new TStickers { Hash = obj.Hash, Stickers = new TVector<IDocument>() };

        return Task.FromResult<IStickers>(r);
    }
}
