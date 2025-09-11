namespace MyTelegram.Messenger.Handlers.LatestLayer.LayerN.Messages;

///<summary>
/// Get info about a stickerset
/// <para>Possible errors</para>
/// Code Type Description
/// 400 EMOTICON_STICKERPACK_MISSING inputStickerSetDice.emoji cannot be empty.
/// 406 STICKERSET_INVALID The provided sticker set is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getStickerSet" />
///</summary>
internal sealed class GetStickerSetHandler(
    IHandlerHelper handlerHelper,
    IRequestConverter<MyTelegram.Schema.Messages.LayerN.RequestGetStickerSet,
        MyTelegram.Schema.Messages.RequestGetStickerSet> dataConverter)
    : ForwardRequestToNewHandler<
            MyTelegram.Schema.Messages.LayerN.RequestGetStickerSet,
            MyTelegram.Schema.Messages.RequestGetStickerSet
        >(handlerHelper, dataConverter)
{
}