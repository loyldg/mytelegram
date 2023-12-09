// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A sponsored message
/// See <a href="https://corefork.telegram.org/constructor/SponsoredMessage" />
///</summary>
[JsonDerivedType(typeof(TSponsoredMessage), nameof(TSponsoredMessage))]
public interface ISponsoredMessage : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the message needs to be labeled as "recommended" instead of "sponsored"
    ///</summary>
    bool Recommended { get; set; }

    ///<summary>
    /// Whether a profile photo bubble should be displayed for this message, like for messages sent in groups. The photo shown in the bubble is obtained either from the peer contained in <code>from_id</code>, or from <code>chat_invite</code>.
    ///</summary>
    bool ShowPeerPhoto { get; set; }

    ///<summary>
    /// Message ID
    ///</summary>
    byte[] RandomId { get; set; }

    ///<summary>
    /// ID of the sender of the message
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer? FromId { get; set; }

    ///<summary>
    /// Information about the chat invite hash specified in <code>chat_invite_hash</code>
    /// See <a href="https://corefork.telegram.org/type/ChatInvite" />
    ///</summary>
    MyTelegram.Schema.IChatInvite? ChatInvite { get; set; }

    ///<summary>
    /// Chat invite
    ///</summary>
    string? ChatInviteHash { get; set; }

    ///<summary>
    /// Optional link to a channel post if <code>from_id</code> points to a channel
    ///</summary>
    int? ChannelPost { get; set; }

    ///<summary>
    /// Parameter for the bot start message if the sponsored chat is a chat with a bot.
    ///</summary>
    string? StartParam { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/SponsoredWebPage" />
    ///</summary>
    MyTelegram.Schema.ISponsoredWebPage? Webpage { get; set; }
    MyTelegram.Schema.IBotApp? App { get; set; }

    ///<summary>
    /// Sponsored message
    ///</summary>
    string Message { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/entities">Message entities for styled text</a>
    /// See <a href="https://corefork.telegram.org/type/MessageEntity" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessageEntity>? Entities { get; set; }
    string? ButtonText { get; set; }

    ///<summary>
    /// If set, contains additional information about the sponsor to be shown along with the message.
    ///</summary>
    string? SponsorInfo { get; set; }

    ///<summary>
    /// If set, contains additional information about the sponsored message to be shown along with the message.
    ///</summary>
    string? AdditionalInfo { get; set; }
}
