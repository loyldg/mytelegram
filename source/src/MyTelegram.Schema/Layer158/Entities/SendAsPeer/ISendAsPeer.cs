// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISendAsPeer : IObject
{
    BitArray Flags { get; set; }
    bool PremiumRequired { get; set; }
    Schema.IPeer Peer { get; set; }
}
