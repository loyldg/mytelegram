// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputFolderPeer : IObject
{
    MyTelegram.Schema.IInputPeer Peer { get; set; }
    int FolderId { get; set; }

}
