// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// How a certain peer reacted to the message
/// See <a href="https://corefork.telegram.org/constructor/MessagePeerReaction" />
///</summary>
[JsonDerivedType(typeof(TMessagePeerReaction), nameof(TMessagePeerReaction))]
public interface IMessagePeerReaction : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the specified <a href="https://corefork.telegram.org/api/reactions">message reaction »</a> should elicit a bigger and longer reaction
    ///</summary>
    bool Big { get; set; }

    ///<summary>
    /// Whether the reaction wasn't yet marked as read by the current user
    ///</summary>
    bool Unread { get; set; }

    ///<summary>
    /// Starting from layer 159, <a href="https://corefork.telegram.org/method/messages.sendReaction">messages.sendReaction</a> will send reactions from the peer (user or channel) specified using <a href="https://corefork.telegram.org/method/messages.saveDefaultSendAs">messages.saveDefaultSendAs</a>. <br>If set, this flag indicates that this reaction was sent by us, even if the <code>peer</code> doesn't point to the current account.
    ///</summary>
    bool My { get; set; }

    ///<summary>
    /// Peer that reacted to the message
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer PeerId { get; set; }

    ///<summary>
    /// When was this reaction added
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// Reaction emoji
    /// See <a href="https://corefork.telegram.org/type/Reaction" />
    ///</summary>
    MyTelegram.Schema.IReaction Reaction { get; set; }
}
