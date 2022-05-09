// ReSharper disable All

namespace MyTelegram.Schema;

public interface IFolderPeer : IObject
{
    MyTelegram.Schema.IPeer Peer { get; set; }
    int FolderId { get; set; }

}
