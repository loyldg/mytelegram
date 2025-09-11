using TStickerSet = MyTelegram.Schema.Messages.TStickerSet;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get info about a stickerset
/// <para>Possible errors</para>
/// Code Type Description
/// 400 EMOTICON_STICKERPACK_MISSING inputStickerSetDice.emoji cannot be empty.
/// 406 STICKERSET_INVALID The provided sticker set is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getStickerSet" />
///</summary>
internal sealed class GetStickerSetHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetStickerSet, MyTelegram.Schema.Messages.IStickerSet>
{
    protected override async Task<MyTelegram.Schema.Messages.IStickerSet> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetStickerSet obj)
    {
        return new TStickerSet
        {
            Packs = [],
            Documents = [],
            Keywords = [],
            Set=new Schema.TStickerSet
            {
                Title = string.Empty,
                ShortName = string.Empty
            }
        };
    }
}
