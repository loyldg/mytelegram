namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get stickers by emoji
/// Possible errors
/// Code Type Description
/// 400 EMOTICON_EMPTY The emoji is empty.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getStickers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetStickers, MyTelegram.Schema.Messages.IStickers>
{
    protected override Task<IStickers> HandleCoreAsync(IRequestInput input, RequestGetStickers obj)
    {
        var r = new TStickers
        {
            Hash = obj.Hash,
            Stickers = []
        };
        return Task.FromResult<IStickers>(r);
    }
}