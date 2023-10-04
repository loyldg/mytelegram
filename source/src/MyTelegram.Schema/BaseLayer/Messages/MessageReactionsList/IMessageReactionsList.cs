// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// List of peers that reacted to a specific message
/// See <a href="https://corefork.telegram.org/constructor/messages.MessageReactionsList" />
///</summary>
public interface IMessageReactionsList : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Total number of reactions matching query
    ///</summary>
    int Count { get; set; }

    ///<summary>
    /// List of peers that reacted to a specific message
    /// See <a href="https://corefork.telegram.org/type/MessagePeerReaction" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessagePeerReaction> Reactions { get; set; }

    ///<summary>
    /// Mentioned chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Mentioned users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

    ///<summary>
    /// If set, indicates the next offset to use to load more results by invoking <a href="https://corefork.telegram.org/method/messages.getMessageReactionsList">messages.getMessageReactionsList</a>.
    ///</summary>
    string? NextOffset { get; set; }
}
