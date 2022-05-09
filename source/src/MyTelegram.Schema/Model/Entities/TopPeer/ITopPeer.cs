// ReSharper disable All

namespace MyTelegram.Schema;

public interface ITopPeer : IObject
{
    MyTelegram.Schema.IPeer Peer { get; set; }
    double Rating { get; set; }

}
