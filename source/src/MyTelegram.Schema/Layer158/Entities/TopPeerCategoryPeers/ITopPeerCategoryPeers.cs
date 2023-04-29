// ReSharper disable All

namespace MyTelegram.Schema;

public interface ITopPeerCategoryPeers : IObject
{
    Schema.ITopPeerCategory Category { get; set; }
    int Count { get; set; }
    TVector<Schema.ITopPeer> Peers { get; set; }
}
