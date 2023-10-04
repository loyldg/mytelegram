// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Peer in a folder
/// See <a href="https://corefork.telegram.org/constructor/InputFolderPeer" />
///</summary>
public interface IInputFolderPeer : IObject
{
    ///<summary>
    /// Peer
    /// See <a href="https://corefork.telegram.org/type/InputPeer" />
    ///</summary>
    MyTelegram.Schema.IInputPeer Peer { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/folders#peer-folders">Peer folder ID, for more info click here</a>
    ///</summary>
    int FolderId { get; set; }
}
