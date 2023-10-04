// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about <a href="https://corefork.telegram.org/api/threads">post comments (for channels) or message replies (for groups)</a>
/// See <a href="https://corefork.telegram.org/constructor/MessageReplies" />
///</summary>
public interface IMessageReplies : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether this constructor contains information about the <a href="https://corefork.telegram.org/api/threads">comment section of a channel post, or a simple message thread</a>
    ///</summary>
    bool Comments { get; set; }

    ///<summary>
    /// Contains the total number of replies in this thread or comment section.
    ///</summary>
    int Replies { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/updates">PTS</a> of the message that started this thread.
    ///</summary>
    int RepliesPts { get; set; }

    ///<summary>
    /// For channel post comments, contains information about the last few comment posters for a specific thread, to show a small list of commenter profile pictures in client previews.
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    TVector<MyTelegram.Schema.IPeer>? RecentRepliers { get; set; }

    ///<summary>
    /// For channel post comments, contains the ID of the associated <a href="https://corefork.telegram.org/api/discussion">discussion supergroup</a>
    ///</summary>
    long? ChannelId { get; set; }

    ///<summary>
    /// ID of the latest message in this thread or comment section.
    ///</summary>
    int? MaxId { get; set; }

    ///<summary>
    /// Contains the ID of the latest read message in this thread or comment section.
    ///</summary>
    int? ReadMaxId { get; set; }
}
