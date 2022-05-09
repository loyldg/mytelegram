// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISetClientDHParamsAnswer : IObject
{
    byte[] Nonce { get; set; }
    byte[] ServerNonce { get; set; }

}
