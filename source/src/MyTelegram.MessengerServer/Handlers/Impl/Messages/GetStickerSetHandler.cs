using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.LayerN;
using MyTelegram.Schema.Messages;
using IStickerSet = MyTelegram.Schema.Messages.IStickerSet;
using TStickerSet = MyTelegram.Schema.TStickerSet;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetStickerSetHandler : RpcResultObjectHandler<RequestGetStickerSet, IStickerSet>,
    IGetStickerSetHandler, IProcessedHandler
{
#pragma warning disable 1998
    protected override async Task<IStickerSet> HandleCoreAsync(
#pragma warning restore 1998
        IRequestInput input,
        RequestGetStickerSet obj)
    {
        //documents count should equal or greater than 2

        //throw new InternalException("stickerset not supported");
        //throw new NotImplementedException();
        //Logger.LogDebug($"get sticker set:{obj.Stickerset.GetType().FullName}");
        //var set = new TStickerSet
        //{
        //    Animated = true,
        //    Id = 1258816259751983,
        //    AccessHash = 1035057606959025459,
        //    Title = "AnimatedEmojies",
        //    ShortName = string.Empty,
        //    Count = 0,
        //};
        //var packs = new TVector<IStickerPack>();
        ////packs.Add(new TStickerPack
        ////{
        ////    Emoticon = "❤",
        ////    Documents = new TVector<long>(1258816259753929)
        ////});
        //var documents = new TVector<IDocument>();
        ////documents.Add(new TDocument
        ////{
        ////    Id = 1258816259753929,
        ////    AccessHash = 6600680225838614916,
        ////    FileReference = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        ////    Date = CurrentDate,
        ////    MimeType = "application/x-tgsticker",
        ////    Size = 56205,
        ////    DcId = 1,
        ////    Attributes = new TVector<IDocumentAttribute>
        ////    {
        ////        new TDocumentAttributeSticker
        ////        {
        ////            Alt = "❤",
        ////            Stickerset = new TInputStickerSetID
        ////            {
        ////                Id = 1258816259751983,
        ////                AccessHash = 1035057606959025459,
        ////            }
        ////        },
        ////        new TDocumentAttributeFilename
        ////        {
        ////            FileName = "AnimatedSticker.tgs"
        ////        }
        ////    }
        ////});

        //MyTelegram.Schema.Messages.IStickerSet r = new MyTelegram.Schema.Messages.TStickerSet
        //{
        //    Set = set,
        //    Packs = packs,
        //    Documents = documents,
        //};

        //return r;
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
            //packs.Add(new TStickerPack
            //{
            //    Emoticon = "❤",
            //    Documents = new TVector<long>(1258816259753929)
            //});
            var documents = new TVector<IDocument>();
            //documents.Add(new TDocument
            //{
            //    Id = 1258816259753929,
            //    AccessHash = 6600680225838614916,
            //    FileReference = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            //    Date = CurrentDate,
            //    MimeType = "application/x-tgsticker",
            //    Size = 56205,
            //    DcId = 1,
            //    Attributes = new TVector<IDocumentAttribute>
            //    {
            //        new TDocumentAttributeSticker
            //        {
            //            Alt = "❤",
            //            Stickerset = new TInputStickerSetID
            //            {
            //                Id = 1258816259751983,
            //                AccessHash = 1035057606959025459,
            //            }
            //        },
            //        new TDocumentAttributeFilename
            //        {
            //            FileName = "AnimatedSticker.tgs"
            //        }
            //    }
            //});

            IStickerSet r = new Schema.Messages.TStickerSet
                { Set = set, Packs = packs, Documents = documents, Keywords = new TVector<IStickerKeyword>() };

            return r;
        }

        if (obj.Stickerset is TInputStickerSetShortName inputStickerSetShortName)
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
                },
                Keywords = new TVector<IStickerKeyword>()
            };

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
                },
                Keywords = new TVector<IStickerKeyword>()
            };

            return r;
        }
    }
}

public class GetStickerSetHandlerLayer134 : RpcResultObjectHandler<RequestGetStickerSetLayer134, IStickerSet>,
    IGetStickerSetHandler, IProcessedHandler
{
#pragma warning disable 1998
    protected override async Task<IStickerSet> HandleCoreAsync(
#pragma warning restore 1998
        IRequestInput input,
        RequestGetStickerSetLayer134 obj)
    {
        //documents count should equal or greater than 2

        //throw new InternalException("stickerset not supported");
        //throw new NotImplementedException();
        //Logger.LogDebug($"get sticker set:{obj.Stickerset.GetType().FullName}");
        //var set = new TStickerSet
        //{
        //    Animated = true,
        //    Id = 1258816259751983,
        //    AccessHash = 1035057606959025459,
        //    Title = "AnimatedEmojies",
        //    ShortName = string.Empty,
        //    Count = 0,
        //};
        //var packs = new TVector<IStickerPack>();
        ////packs.Add(new TStickerPack
        ////{
        ////    Emoticon = "❤",
        ////    Documents = new TVector<long>(1258816259753929)
        ////});
        //var documents = new TVector<IDocument>();
        ////documents.Add(new TDocument
        ////{
        ////    Id = 1258816259753929,
        ////    AccessHash = 6600680225838614916,
        ////    FileReference = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        ////    Date = CurrentDate,
        ////    MimeType = "application/x-tgsticker",
        ////    Size = 56205,
        ////    DcId = 1,
        ////    Attributes = new TVector<IDocumentAttribute>
        ////    {
        ////        new TDocumentAttributeSticker
        ////        {
        ////            Alt = "❤",
        ////            Stickerset = new TInputStickerSetID
        ////            {
        ////                Id = 1258816259751983,
        ////                AccessHash = 1035057606959025459,
        ////            }
        ////        },
        ////        new TDocumentAttributeFilename
        ////        {
        ////            FileName = "AnimatedSticker.tgs"
        ////        }
        ////    }
        ////});

        //MyTelegram.Schema.Messages.IStickerSet r = new MyTelegram.Schema.Messages.TStickerSet
        //{
        //    Set = set,
        //    Packs = packs,
        //    Documents = documents,
        //};

        //return r;
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
            //packs.Add(new TStickerPack
            //{
            //    Emoticon = "❤",
            //    Documents = new TVector<long>(1258816259753929)
            //});
            var documents = new TVector<IDocument>();
            //documents.Add(new TDocument
            //{
            //    Id = 1258816259753929,
            //    AccessHash = 6600680225838614916,
            //    FileReference = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            //    Date = CurrentDate,
            //    MimeType = "application/x-tgsticker",
            //    Size = 56205,
            //    DcId = 1,
            //    Attributes = new TVector<IDocumentAttribute>
            //    {
            //        new TDocumentAttributeSticker
            //        {
            //            Alt = "❤",
            //            Stickerset = new TInputStickerSetID
            //            {
            //                Id = 1258816259751983,
            //                AccessHash = 1035057606959025459,
            //            }
            //        },
            //        new TDocumentAttributeFilename
            //        {
            //            FileName = "AnimatedSticker.tgs"
            //        }
            //    }
            //});

            IStickerSet r = new Schema.Messages.TStickerSet
                { Set = set, Packs = packs, Documents = documents, Keywords = new TVector<IStickerKeyword>() };

            return r;
        }

        if (obj.Stickerset is TInputStickerSetShortName inputStickerSetShortName)
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
                },
                Keywords = new TVector<IStickerKeyword>()
            };

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
                },
                Keywords = new TVector<IStickerKeyword>()
            };

            return r;
        }
    }
}