// ReSharper disable All

namespace MyTelegram.Schema;

public interface IFolderPeer : IObject
{
    Schema.IPeer Peer { get; set; }
    int FolderId { get; set; }
}
