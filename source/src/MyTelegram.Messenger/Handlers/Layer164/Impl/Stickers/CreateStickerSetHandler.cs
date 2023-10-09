// ReSharper disable All

namespace MyTelegram.Handlers.Stickers;

///<summary>
/// Create a stickerset, bots only.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PACK_SHORT_NAME_INVALID Short pack name invalid.
/// 400 PACK_SHORT_NAME_OCCUPIED A stickerpack with this name already exists.
/// 400 PACK_TITLE_INVALID The stickerpack title is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 STICKERS_EMPTY No sticker provided.
/// 400 STICKER_EMOJI_INVALID Sticker emoji invalid.
/// 400 STICKER_FILE_INVALID Sticker file invalid.
/// 400 STICKER_GIF_DIMENSIONS The specified video sticker has invalid dimensions.
/// 400 STICKER_PNG_DIMENSIONS Sticker png dimensions invalid.
/// 400 STICKER_PNG_NOPNG One of the specified stickers is not a valid PNG file.
/// 400 STICKER_TGS_NODOC You must send the animated sticker as a document.
/// 400 STICKER_TGS_NOTGS Invalid TGS sticker provided.
/// 400 STICKER_THUMB_PNG_NOPNG Incorrect stickerset thumb file provided, PNG / WEBP expected.
/// 400 STICKER_THUMB_TGS_NOTGS Incorrect stickerset TGS thumb file provided.
/// 400 STICKER_VIDEO_BIG The specified video sticker is too big.
/// 400 STICKER_VIDEO_NODOC You must send the video sticker as a document.
/// 400 STICKER_VIDEO_NOWEBM The specified video sticker is not in webm format.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/stickers.createStickerSet" />
///</summary>
internal sealed class CreateStickerSetHandler : RpcResultObjectHandler<MyTelegram.Schema.Stickers.RequestCreateStickerSet, MyTelegram.Schema.Messages.IStickerSet>,
    Stickers.ICreateStickerSetHandler
{
    protected override Task<MyTelegram.Schema.Messages.IStickerSet> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stickers.RequestCreateStickerSet obj)
    {
        throw new NotImplementedException();
    }
}
