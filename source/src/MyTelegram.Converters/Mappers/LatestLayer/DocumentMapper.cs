using System.Diagnostics.CodeAnalysis;

namespace MyTelegram.Converters.Mappers.LatestLayer;

internal sealed class DocumentMapper
    : IObjectMapper<IDocumentReadModel, TDocument>,
        IObjectMapper<TDocument, TDocument>,
        ILayeredMapper,
        ITransientDependency
{
    public int Layer => Layers.LayerLatest;


    public TDocument Map(IDocumentReadModel source)
    {
        return Map(source, new TDocument());
    }

    public TDocument Map(
        IDocumentReadModel source,
        TDocument destination
    )
    {
        destination.Id = source.DocumentId;
        destination.AccessHash = source.AccessHash;
        destination.FileReference = source.FileReference;
        destination.Date = source.Date;
        destination.MimeType = source.MimeType;
        destination.Size = source.Size;
        //destination.Thumbs = source.Thumbs;

        if (source.Thumbs != null)
        {
            destination.Thumbs =
            [
                .. source.Thumbs!.Select(p =>
                {
                    switch (p.Type)
                    {
                        case "i":
                            return (IPhotoSize)new TPhotoStrippedSize { Type = p.Type, Bytes = p.Bytes?? Array.Empty<byte>()};
                        case "j":
                            return new TPhotoPathSize { Type = p.Type, Bytes = p.Bytes?? Array.Empty<byte>() };
                    }

                    return new TPhotoSize
                    {
                        H = p.H,
                        Size = (int)p.Size,
                        Type = p.Type,
                        W = p.W
                    };
                })
            ];
        }

        //destination.VideoThumbs = source.VideoThumbs;

        if (source.VideoThumbs != null)
        {
            destination.VideoThumbs =
            [
                .. source.VideoThumbs.Select(p => new TVideoSize
                {
                    H = p.H,
                    W = p.W,
                    Size = (int)p.Size,
                    Type = p.Type,
                    VideoStartTs = p.VideoStartTs
                })
            ];
        }

        destination.DcId = source.DcId;
        //destination.Attributes = source.Attributes;

        destination.Attributes = source.Attributes2 != null
            ? [.. source.Attributes2]
            : source.Attributes.ToTObject<TVector<IDocumentAttribute>>();

        return destination;
    }

    [return: NotNullIfNotNull("source")]
    public TDocument? Map(TDocument source)
    {
        return Map(source, new TDocument());
    }

    [return: NotNullIfNotNull("source")]
    public TDocument? Map(TDocument source, TDocument destination)
    {
        destination.Id = source.Id;
        destination.AccessHash = source.AccessHash;
        destination.FileReference = source.FileReference;
        destination.Date = source.Date;
        destination.MimeType = source.MimeType;
        destination.Size = source.Size;
        destination.Thumbs = source.Thumbs;
        destination.VideoThumbs = source.VideoThumbs;
        destination.DcId = source.DcId;
        destination.Attributes = source.Attributes;

        foreach (var documentAttribute in destination.Attributes)
        {
            switch (documentAttribute)
            {
                case TDocumentAttributeCustomEmoji documentAttributeCustomEmoji:
                    switch (documentAttributeCustomEmoji.Stickerset)
                    {
                        case TInputStickerSetID inputStickerSetId:
                            documentAttributeCustomEmoji.Stickerset = new TInputStickerSetID
                            {
                                Id = inputStickerSetId.Id,
                                AccessHash = inputStickerSetId.AccessHash
                            };
                            break;
                    }
                    break;
                case TDocumentAttributeSticker documentAttributeSticker:
                    switch (documentAttributeSticker.Stickerset)
                    {
                        case TInputStickerSetID inputStickerSetId:
                            documentAttributeSticker.Stickerset = new TInputStickerSetID
                            {
                                Id = inputStickerSetId.Id,
                                AccessHash = inputStickerSetId.AccessHash
                            };
                            break;
                    }
                    break;
            }
        }

        return destination;
    }
}