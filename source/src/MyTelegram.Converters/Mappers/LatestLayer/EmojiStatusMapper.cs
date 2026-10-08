namespace MyTelegram.Converters.Mappers.LatestLayer;

internal sealed class EmojiStatusMapper
    : IObjectMapper<EmojiStatus, TEmojiStatus>,
        ILayeredMapper,
        ITransientDependency
{
    public int Layer => Layers.LayerLatest;
    
    public TEmojiStatus Map(EmojiStatus source)
    {
        return Map(source, new TEmojiStatus());
    }

    public TEmojiStatus Map(
        EmojiStatus source,
        TEmojiStatus destination
    )
    {
        destination.DocumentId = source.DocumentId;
        destination.Until = source.Until;

        return destination;
    }
}