// ReSharper disable All

namespace MyTelegram.Schema;

public interface ICdnConfig : IObject
{
    TVector<MyTelegram.Schema.ICdnPublicKey> PublicKeys { get; set; }
}
