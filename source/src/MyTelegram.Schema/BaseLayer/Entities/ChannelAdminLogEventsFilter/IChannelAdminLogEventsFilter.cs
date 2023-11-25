// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Filter for fetching events in the channel admin log
/// See <a href="https://corefork.telegram.org/constructor/ChannelAdminLogEventsFilter" />
///</summary>
[JsonDerivedType(typeof(TChannelAdminLogEventsFilter), nameof(TChannelAdminLogEventsFilter))]
public interface IChannelAdminLogEventsFilter : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionParticipantJoin">Join events</a>
    ///</summary>
    bool Join { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionParticipantLeave">Leave events</a>
    ///</summary>
    bool Leave { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionParticipantInvite">Invite events</a>
    ///</summary>
    bool Invite { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionParticipantToggleBan">Ban events</a>
    ///</summary>
    bool Ban { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionParticipantToggleBan">Unban events</a>
    ///</summary>
    bool Unban { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionParticipantToggleBan">Kick events</a>
    ///</summary>
    bool Kick { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionParticipantToggleBan">Unkick events</a>
    ///</summary>
    bool Unkick { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionParticipantToggleAdmin">Admin promotion events</a>
    ///</summary>
    bool Promote { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionParticipantToggleAdmin">Admin demotion events</a>
    ///</summary>
    bool Demote { get; set; }

    ///<summary>
    /// Info change events (when <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionChangeAbout">about</a>, <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionChangeLinkedChat">linked chat</a>, <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionChangeLocation">location</a>, <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionChangePhoto">photo</a>, <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionChangeStickerSet">stickerset</a>, <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionChangeTitle">title</a> or <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionChangeUsername">username</a> data of a channel gets modified)
    ///</summary>
    bool Info { get; set; }

    ///<summary>
    /// Settings change events (<a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionToggleInvites">invites</a>, <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionTogglePreHistoryHidden">hidden prehistory</a>, <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionToggleSignatures">signatures</a>, <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionDefaultBannedRights">default banned rights</a>)
    ///</summary>
    bool Settings { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionUpdatePinned">Message pin events</a>
    ///</summary>
    bool Pinned { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionEditMessage">Message edit events</a>
    ///</summary>
    bool Edit { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionDeleteMessage">Message deletion events</a>
    ///</summary>
    bool Delete { get; set; }

    ///<summary>
    /// Group call events
    ///</summary>
    bool GroupCall { get; set; }

    ///<summary>
    /// Invite events
    ///</summary>
    bool Invites { get; set; }

    ///<summary>
    /// A message was posted in a channel
    ///</summary>
    bool Send { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/forum">Forum</a>-related events
    ///</summary>
    bool Forums { get; set; }
}
