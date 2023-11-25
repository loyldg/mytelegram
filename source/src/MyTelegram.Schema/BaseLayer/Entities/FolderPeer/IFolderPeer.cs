// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Peer associated to folder
/// See <a href="https://corefork.telegram.org/constructor/FolderPeer" />
///</summary>
[JsonDerivedType(typeof(TFolderPeer), nameof(TFolderPeer))]
public interface IFolderPeer : IObject
{
    ///<summary>
    /// Folder peer info
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer Peer { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/folders#peer-folders">Peer folder ID, for more info click here</a>
    ///</summary>
    int FolderId { get; set; }
}
