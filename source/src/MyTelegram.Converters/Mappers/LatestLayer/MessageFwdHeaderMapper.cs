namespace MyTelegram.Converters.Mappers.LatestLayer;

internal sealed class MessageFwdHeaderMapper
    : IObjectMapper<MessageFwdHeader, TMessageFwdHeader>,
        ILayeredMapper,
        ITransientDependency
{
    public int Layer => Layers.LayerLatest;
    

    public TMessageFwdHeader Map(MessageFwdHeader source)
    {
        return Map(source, new TMessageFwdHeader());
    }

    public TMessageFwdHeader Map(
        MessageFwdHeader source,
        TMessageFwdHeader destination
    )
    {
        destination.Imported = source.Imported;
        destination.SavedOut = source.SavedOut;
        destination.FromId = source.FromId.ToPeer();
        destination.FromName = source.FromName;
        destination.Date = source.Date;
        destination.ChannelPost = source.ChannelPost;
        destination.PostAuthor = source.PostAuthor;
        destination.SavedFromPeer = source.SavedFromPeer.ToPeer();
        destination.SavedFromMsgId = source.SavedFromMsgId;
        destination.SavedFromId = source.SavedFromId.ToPeer();
        destination.SavedFromName = source.SavedFromName;
        destination.SavedDate = source.SavedDate;
        destination.PsaType = source.PsaType;

        if (destination.SavedFromMsgId == null)
        {
            destination.SavedFromPeer = null;
        }

        if (destination.SavedFromPeer == null)
        {
            destination.SavedFromMsgId = null;
        }

        return destination;
    }
}