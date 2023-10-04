// ReSharper disable All
using IStickerSet = MyTelegram.Schema.Messages.IStickerSet;
using TStickerSet = MyTelegram.Schema.TStickerSet;

namespace MyTelegram.Handlers.Messages.LayerN;

///<summary>
/// Get info about a stickerset
/// <para>Possible errors</para>
/// Code Type Description
/// 400 EMOTICON_STICKERPACK_MISSING inputStickerSetDice.emoji cannot be empty.
/// 406 STICKERSET_INVALID The provided sticker set is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getStickerSet" />
///</summary>
internal sealed class GetStickerSetHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.LayerN.RequestGetStickerSet, MyTelegram.Schema.Messages.IStickerSet>,
    Messages.LayerN.IGetStickerSetHandler, IProcessedHandler
{
    protected override async Task<MyTelegram.Schema.Messages.IStickerSet> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.LayerN.RequestGetStickerSet obj)
    {
        if (obj.Stickerset is TInputStickerSetAnimatedEmoji)
        {
            var set = new TStickerSet
            {
                Animated = true,
                Id = 0,
                AccessHash = 1035057606959025459,
                Title = "AnimatedEmojies",
                //ShortName = "AnimatedEmojies",
                ShortName = "tg_placeholders",
                Count = 0
            };
            var packs = new TVector<IStickerPack>();

            var documents = new TVector<IDocument>();

            IStickerSet r = new Schema.Messages.TStickerSet { Set = set, Packs = packs, Documents = documents };

            return r;
        }

        if (obj.Stickerset is TInputStickerSetShortName inputStickerSetShortName)
        {
            //Logger.LogDebug($"get sticker short name:{inputStickerSetShortName.ShortName}");
            return new Schema.Messages.TStickerSet
            {
                Documents = new TVector<IDocument>(),
                Packs = new TVector<IStickerPack>(),
                Set = new TStickerSet
                {
                    Animated = true,
                    Count = 0,
                    Masks = false,
                    Archived = true,
                    Official = false,
                    InstalledDate = CurrentDate,
                    Id = 1,
                    ShortName = inputStickerSetShortName.ShortName,
                    AccessHash = 1494194797110569295,
                    Title = "Animated Emojies",
                    Thumbs = new TVector<IPhotoSize>()
                }
            };
        }

        {
            IStickerSet r = new Schema.Messages.TStickerSet
            {
                Documents = new TVector<IDocument>(),
                Packs = new TVector<IStickerPack>(),
                Set = new TStickerSet
                {
                    Animated = true,
                    Count = 0,
                    Masks = false,
                    Archived = false,
                    Official = true,
                    InstalledDate = CurrentDate,
                    Id = 1,
                    ShortName = "tg_placeholders",
                    AccessHash = 1494194797110569295,
                    Title = "Animated Emojies",
                    Thumbs = new TVector<IPhotoSize>()
                }
            };

            return r;
        }
    }
}
