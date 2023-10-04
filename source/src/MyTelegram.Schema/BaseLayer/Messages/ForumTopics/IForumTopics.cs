// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Contains information about multiple <a href="https://corefork.telegram.org/api/forum#forum-topics">forum topics</a>
/// See <a href="https://corefork.telegram.org/constructor/messages.ForumTopics" />
///</summary>
public interface IForumTopics : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the returned topics are ordered by creation date; if set, pagination by <code>next_offset</code> should use <a href="https://corefork.telegram.org/constructor/forumTopic">forumTopic</a>.<code>date</code>; otherwise topics are ordered by the last message date, so paginate by the <code>date</code> of the <a href="https://corefork.telegram.org/type/Message">message</a> referenced by <a href="https://corefork.telegram.org/constructor/forumTopic">forumTopic</a>.<code>top_message</code>.
    ///</summary>
    bool OrderByCreateDate { get; set; }

    ///<summary>
    /// Total number of topics matching query; may be less than the topics contained in <code>topics</code>, in which case <a href="https://corefork.telegram.org/api/offsets">pagination</a> is required.
    ///</summary>
    int Count { get; set; }

    ///<summary>
    /// Forum topics
    /// See <a href="https://corefork.telegram.org/type/ForumTopic" />
    ///</summary>
    TVector<MyTelegram.Schema.IForumTopic> Topics { get; set; }

    ///<summary>
    /// Related messages (contains the messages mentioned by <a href="https://corefork.telegram.org/constructor/forumTopic">forumTopic</a>.<code>top_message</code>).
    /// See <a href="https://corefork.telegram.org/type/Message" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessage> Messages { get; set; }

    ///<summary>
    /// Related chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Related users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/updates">Event count after generation</a>
    ///</summary>
    int Pts { get; set; }
}
