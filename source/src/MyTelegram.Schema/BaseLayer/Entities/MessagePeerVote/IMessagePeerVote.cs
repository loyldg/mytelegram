// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// How a user voted in a poll
/// See <a href="https://corefork.telegram.org/constructor/MessagePeerVote" />
///</summary>
[JsonDerivedType(typeof(TMessagePeerVote), nameof(TMessagePeerVote))]
[JsonDerivedType(typeof(TMessagePeerVoteInputOption), nameof(TMessagePeerVoteInputOption))]
[JsonDerivedType(typeof(TMessagePeerVoteMultiple), nameof(TMessagePeerVoteMultiple))]
public interface IMessagePeerVote : IObject
{
    ///<summary>
    /// Peer ID
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer Peer { get; set; }

    ///<summary>
    /// When did the peer cast their votes
    ///</summary>
    int Date { get; set; }
}
