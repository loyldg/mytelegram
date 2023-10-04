// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Global privacy settings
/// See <a href="https://corefork.telegram.org/constructor/GlobalPrivacySettings" />
///</summary>
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
    /// &nbsp;
    ///</summary>
    bool KeepArchivedUnmuted { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool KeepArchivedFolders { get; set; }
}
