// ReSharper disable All

namespace MyTelegram.Schema;

public interface ICdnConfig : IObject
{
    TVector<Schema.ICdnPublicKey> PublicKeys { get; set; }
}
