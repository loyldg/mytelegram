// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents the rights of an admin in a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>.
/// See <a href="https://corefork.telegram.org/constructor/ChatAdminRights" />
///</summary>
[JsonDerivedType(typeof(TChatAdminRights), nameof(TChatAdminRights))]
public interface IChatAdminRights : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If set, allows the admin to modify the description of the <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
    ///</summary>
    bool ChangeInfo { get; set; }

    ///<summary>
    /// If set, allows the admin to post messages in the <a href="https://corefork.telegram.org/api/channel">channel</a>
    ///</summary>
    bool PostMessages { get; set; }

    ///<summary>
    /// If set, allows the admin to also edit messages from other admins in the <a href="https://corefork.telegram.org/api/channel">channel</a>
    ///</summary>
    bool EditMessages { get; set; }

    ///<summary>
    /// If set, allows the admin to also delete messages from other admins in the <a href="https://corefork.telegram.org/api/channel">channel</a>
    ///</summary>
    bool DeleteMessages { get; set; }

    ///<summary>
    /// If set, allows the admin to ban users from the <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
    ///</summary>
    bool BanUsers { get; set; }

    ///<summary>
    /// If set, allows the admin to invite users in the <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
    ///</summary>
    bool InviteUsers { get; set; }

    ///<summary>
    /// If set, allows the admin to pin messages in the <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
    ///</summary>
    bool PinMessages { get; set; }

    ///<summary>
    /// If set, allows the admin to add other admins with the same (or more limited) permissions in the <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
    ///</summary>
    bool AddAdmins { get; set; }

    ///<summary>
    /// Whether this admin is anonymous
    ///</summary>
    bool Anonymous { get; set; }

    ///<summary>
    /// If set, allows the admin to change group call/livestream settings
    ///</summary>
    bool ManageCall { get; set; }

    ///<summary>
    /// Set this flag if none of the other flags are set, but you still want the user to be an admin: if this or any of the other flags are set, the admin can get the chat <a href="https://corefork.telegram.org/api/recent-actions">admin log</a>, get <a href="https://corefork.telegram.org/api/stats">chat statistics</a>, get <a href="https://corefork.telegram.org/api/stats">message statistics in channels</a>, get channel members, see anonymous administrators in supergroups and ignore slow mode.
    ///</summary>
    bool Other { get; set; }

    ///<summary>
    /// If set, allows the admin to create, delete or modify <a href="https://corefork.telegram.org/api/forum#forum-topics">forum topics »</a>.
    ///</summary>
    bool ManageTopics { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool PostStories { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool EditStories { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool DeleteStories { get; set; }
}
