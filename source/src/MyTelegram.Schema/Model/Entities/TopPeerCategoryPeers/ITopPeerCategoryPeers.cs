// ReSharper disable All

namespace MyTelegram.Schema;

public interface ITopPeerCategoryPeers : IObject
{
    MyTelegram.Schema.ITopPeerCategory Category { get; set; }
    int Count { get; set; }
    TVector<MyTelegram.Schema.ITopPeer> Peers { get; set; }

}
