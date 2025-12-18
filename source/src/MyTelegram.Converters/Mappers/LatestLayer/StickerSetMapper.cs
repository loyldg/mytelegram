namespace MyTelegram.Converters.Mappers.LatestLayer;

internal sealed class StickerSetMapper
    : IObjectMapper<IStickerSetReadModel, Schema.TStickerSet>,
        ILayeredMapper,
        ITransientDependency
{
    private const int DcId = 1;
    private const int ThumbDcId = 1;
    public int Layer => Layers.LayerLatest;


    public Schema.TStickerSet Map(IStickerSetReadModel source)
    {
        return Map(source, new Schema.TStickerSet());
    }

    public Schema.TStickerSet Map(
        IStickerSetReadModel source,
        Schema.TStickerSet destination
    )
    {
        int? thumbDcId = null;
        int? thumbVersion = null;
        if (source.Thumbs?.Count > 0)
        {
            thumbDcId = ThumbDcId;
            thumbVersion = source.ThumbVersion;
        }

        //destination.Archived = source.Archived;
        destination.Official = true; // source.Official;
        destination.Masks = source.Masks;
        destination.Emojis = source.Emojis;
        destination.TextColor = source.TextColor;
        destination.ChannelEmojiStatus = source.ChannelEmojiStatus;
        //destination.Creator = source.Creator;
        //destination.InstalledDate = source.InstalledDate;
        destination.Id = source.StickerSetId;
        destination.AccessHash = source.AccessHash;
        destination.Title = source.Title;
        destination.ShortName = source.ShortName;
        destination.Thumbs = source.Thumbs == null
            ? null
            : new TVector<IPhotoSize>(source.Thumbs.Select(p =>
            {
                switch (p.Type)
                {
                    case "i":
                        return (IPhotoSize)new TPhotoStrippedSize { Type = p.Type, Bytes = p.Bytes ?? ReadOnlyMemory<byte>.Empty };
                    case "j":
                        return new TPhotoPathSize { Type = p.Type, Bytes = p.Bytes ?? ReadOnlyMemory<byte>.Empty };
                }

                return new TPhotoSize
                {
                    H = p.H,
                    Size = (int)p.Size,
                    Type = p.Type,
                    W = p.W
                };
            }));
        destination.ThumbDcId = thumbDcId;
        destination.ThumbVersion = thumbVersion;
        destination.ThumbDocumentId = source.ThumbDocumentId;
        destination.Count = source.Count;
        if (destination.Count == 0)
        {
            destination.Count = source.StickerDocumentIds.Count;
        }
        //destination.Hash = source.Hash;

        return destination;
    }
}