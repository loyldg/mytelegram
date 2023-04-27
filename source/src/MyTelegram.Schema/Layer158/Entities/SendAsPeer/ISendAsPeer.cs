// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISendAsPeer : IObject
{
    BitArray Flags { get; set; }
    bool PremiumRequired { get; set; }
    MyTelegram.Schema.IPeer Peer { get; set; }
}
