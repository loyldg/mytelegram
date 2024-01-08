// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// How users voted in a poll
/// See <a href="https://corefork.telegram.org/constructor/messages.VotesList" />
///</summary>
[JsonDerivedType(typeof(TVotesList), nameof(TVotesList))]
public interface IVotesList : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Total number of votes for all options (or only for the chosen <code>option</code>, if provided to <a href="https://corefork.telegram.org/method/messages.getPollVotes">messages.getPollVotes</a>)
    ///</summary>
    int Count { get; set; }

    ///<summary>
    /// Vote info for each user
    /// See <a href="https://corefork.telegram.org/type/MessagePeerVote" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessagePeerVote> Votes { get; set; }

    ///<summary>
    /// Mentioned chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Info about users that voted in the poll
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

    ///<summary>
    /// Offset to use with the next <a href="https://corefork.telegram.org/method/messages.getPollVotes">messages.getPollVotes</a> request, empty string if no more results are available.
    ///</summary>
    string? NextOffset { get; set; }
}
