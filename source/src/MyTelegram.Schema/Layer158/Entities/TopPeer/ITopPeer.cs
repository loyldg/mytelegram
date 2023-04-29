// ReSharper disable All

namespace MyTelegram.Schema;

public interface ITopPeer : IObject
{
    Schema.IPeer Peer { get; set; }
    double Rating { get; set; }
}
