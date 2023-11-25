// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Info about a message thread
/// See <a href="https://corefork.telegram.org/constructor/messages.DiscussionMessage" />
///</summary>
[JsonDerivedType(typeof(TDiscussionMessage), nameof(TDiscussionMessage))]
public interface IDiscussionMessage : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Discussion messages
    /// See <a href="https://corefork.telegram.org/type/Message" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessage> Messages { get; set; }

    ///<summary>
    /// Message ID of latest reply in this <a href="https://corefork.telegram.org/api/threads">thread</a>
    ///</summary>
    int? MaxId { get; set; }

    ///<summary>
    /// Message ID of latest read incoming message in this <a href="https://corefork.telegram.org/api/threads">thread</a>
    ///</summary>
    int? ReadInboxMaxId { get; set; }

    ///<summary>
    /// Message ID of latest read outgoing message in this <a href="https://corefork.telegram.org/api/threads">thread</a>
    ///</summary>
    int? ReadOutboxMaxId { get; set; }

    ///<summary>
    /// Number of unread messages
    ///</summary>
    int UnreadCount { get; set; }

    ///<summary>
    /// Chats mentioned in constructor
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Users mentioned in constructor
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
