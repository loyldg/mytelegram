// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about a forwarded message
/// See <a href="https://corefork.telegram.org/constructor/MessageFwdHeader" />
///</summary>
public interface IMessageFwdHeader : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether this message was <a href="https://corefork.telegram.org/api/import">imported from a foreign chat service, click here for more info »</a>
    ///</summary>
    bool Imported { get; set; }

    ///<summary>
    /// The ID of the user that originally sent the message
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer? FromId { get; set; }

    ///<summary>
    /// The name of the user that originally sent the message
    ///</summary>
    string? FromName { get; set; }

    ///<summary>
    /// When was the message originally sent
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// ID of the channel message that was forwarded
    ///</summary>
    int? ChannelPost { get; set; }

    ///<summary>
    /// For channels and if signatures are enabled, author of the channel message
    ///</summary>
    string? PostAuthor { get; set; }

    ///<summary>
    /// Only for messages forwarded to the current user (inputPeerSelf), full info about the user/channel that originally sent the message
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer? SavedFromPeer { get; set; }

    ///<summary>
    /// Only for messages forwarded to the current user (inputPeerSelf), ID of the message that was forwarded from the original user/channel
    ///</summary>
    int? SavedFromMsgId { get; set; }

    ///<summary>
    /// PSA type
    ///</summary>
    string? PsaType { get; set; }
}
