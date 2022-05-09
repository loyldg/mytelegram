// ReSharper disable All

namespace MyTelegram.Schema;

public interface IResPQ : IObject
{
    byte[] Nonce { get; set; }
    byte[] ServerNonce { get; set; }
    byte[] Pq { get; set; }
    TVector<long> ServerPublicKeyFingerprints { get; set; }

}
