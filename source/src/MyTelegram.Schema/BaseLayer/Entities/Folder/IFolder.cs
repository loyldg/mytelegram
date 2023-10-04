// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A folder
/// See <a href="https://corefork.telegram.org/constructor/Folder" />
///</summary>
public interface IFolder : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Automatically add new channels to this folder
    ///</summary>
    bool AutofillNewBroadcasts { get; set; }

    ///<summary>
    /// Automatically add joined new public supergroups to this folder
    ///</summary>
    bool AutofillPublicGroups { get; set; }

    ///<summary>
    /// Automatically add new private chats to this folder
    ///</summary>
    bool AutofillNewCorrespondents { get; set; }

    ///<summary>
    /// Folder ID
    ///</summary>
    int Id { get; set; }

    ///<summary>
    /// Folder title
    ///</summary>
    string Title { get; set; }

    ///<summary>
    /// Folder picture
    /// See <a href="https://corefork.telegram.org/type/ChatPhoto" />
    ///</summary>
    MyTelegram.Schema.IChatPhoto? Photo { get; set; }
}
