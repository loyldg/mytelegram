// ReSharper disable All

namespace MyTelegram.Schema;

public interface ICdnPublicKey : IObject
{
    int DcId { get; set; }
    string PublicKey { get; set; }

}
