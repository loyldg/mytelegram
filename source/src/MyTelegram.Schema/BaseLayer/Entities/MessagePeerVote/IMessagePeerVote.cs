// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/MessagePeerVote" />
///</summary>
[JsonDerivedType(typeof(TMessagePeerVote), nameof(TMessagePeerVote))]
[JsonDerivedType(typeof(TMessagePeerVoteInputOption), nameof(TMessagePeerVoteInputOption))]
[JsonDerivedType(typeof(TMessagePeerVoteMultiple), nameof(TMessagePeerVoteMultiple))]
public interface IMessagePeerVote : IObject
{
    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer Peer { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int Date { get; set; }
}
