// ReSharper disable All

namespace MyTelegram.Schema;

public interface IDialog : IObject
{
    BitArray Flags { get; set; }
    bool Pinned { get; set; }
    MyTelegram.Schema.IPeer Peer { get; set; }
    int TopMessage { get; set; }

}
