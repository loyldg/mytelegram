// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPQInnerData : IObject
{
    byte[] Pq { get; set; }
    byte[] P { get; set; }
    byte[] Q { get; set; }
    byte[] Nonce { get; set; }
    byte[] ServerNonce { get; set; }
    byte[] NewNonce { get; set; }
    int Dc { get; set; }
}
