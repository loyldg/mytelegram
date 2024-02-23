namespace MyTelegram.Domain.ValueObjects;

public class MessageFwdHeader : ValueObject
{
    public MessageFwdHeader(
        bool imported,
        bool savedOut,
        Peer fromId,
        string? fromName,
        int channelPost,
        string postAuthor,
        int date,
        Peer? savedFromPeer,
        int? savedFromMsgId,
        Peer? savedFromId,
        string? savedFromName,
        int? savedDate,
        string? psaType
        )
    {
        Imported = imported;
        SavedOut = savedOut;
        FromId = fromId;
        FromName = fromName;
        ChannelPost = channelPost;
        PostAuthor = postAuthor;
        Date = date;
        SavedFromPeer = savedFromPeer;
        SavedFromMsgId = savedFromMsgId;
        SavedFromId = savedFromId;
        SavedFromName = savedFromName;
        SavedDate = savedDate;
        PsaType = psaType;
    }

    /// <summary>
    ///     ID of the channel message that was forwarded
    /// </summary>
    public int ChannelPost { get; init; }

    /// <summary>
    ///     When was the message originally sent
    /// </summary>
    public int Date { get; init; }

    public bool Imported { get; init; }
    public bool SavedOut { get; init; }

    /// <summary>
    ///     The ID of the user that originally sent the message
    /// </summary>
    public Peer FromId { get; init; }

    /// <summary>
    ///     The name of the user that originally sent the message
    /// </summary>
    public string? FromName { get; init; }

    /// <summary>
    ///     For channels and if signatures are enabled, author of the channel message
    /// </summary>
    public string PostAuthor { get; init; }

    /// <summary>
    ///     Only for messages forwarded to the current user (inputPeerSelf), ID of the message that was forwarded from the
    ///     original user/channel
    /// </summary>
    public int? SavedFromMsgId { get; init; }

    public Peer? SavedFromId { get; init; }
    public string? SavedFromName { get; init; }
    public int? SavedDate { get; init; }
    public string? PsaType { get; }

    /// <summary>
    ///     Only for messages forwarded to the current user (inputPeerSelf), full info about the user/channel that originally
    ///     sent the message
    /// </summary>
    public Peer? SavedFromPeer { get; init; }
}
