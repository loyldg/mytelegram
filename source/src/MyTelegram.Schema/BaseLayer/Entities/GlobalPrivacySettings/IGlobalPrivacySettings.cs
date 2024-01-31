// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Global privacy settings
/// See <a href="https://corefork.telegram.org/constructor/GlobalPrivacySettings" />
///</summary>
[JsonDerivedType(typeof(TGlobalPrivacySettings), nameof(TGlobalPrivacySettings))]
public interface IGlobalPrivacySettings : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether to archive and mute new chats from non-contacts
    ///</summary>
    bool ArchiveAndMuteNewNoncontactPeers { get; set; }

    ///<summary>
    /// Whether unmuted chats will be kept in the Archive chat list when they get a new message.
    ///</summary>
    bool KeepArchivedUnmuted { get; set; }

    ///<summary>
    /// Whether unmuted chats that are always included or pinned in a <a href="https://corefork.telegram.org/api/folders">folder</a>, will be kept in the Archive chat list when they get a new message. Ignored if <code>keep_archived_unmuted</code> is set.
    ///</summary>
    bool KeepArchivedFolders { get; set; }
    bool HideReadMarks { get; set; }
    bool NewNoncontactPeersRequirePremium { get; set; }
}
